using GestorTareas.Dominio;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DbControllerTareas : ControllerBase
{
    private readonly GestorTareasDbContext _dbContext;

    public DbControllerTareas(GestorTareasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("list")]
    public async Task<ActionResult> ListTareasAsync(
        CancellationToken cancellationToken = default // <-- No te olvides del token de cancelación
    )
    {
        // Cargamos todas las tareas de la base de datos
        var tareas = await _dbContext.Tareas

             // Indicamos que queremos cargar también las etiquetas relacionadas de cada Tarea
             // Esto añade un JOIN a la consulta SQL. Más o menos:
             //  SELECT
             //      *
             //  FROM
             //      [dbo].[Tareas] AS [T]
             //          LEFT JOIN [dbo].[Etiquetas] AS [E] ON [E].[TareaId] = [T].[Id];
             .Include(t => t.Etiquetas)

             // Como no tenemos que aplicar ningún filtro ni operación sobre
             // el resultado, podemos obtener la lista directamente
             .ToListAsync(cancellationToken);


        // Si no hay tareas, en vez de mostrar un código 400 (Bad Request),
        // se debería mostrar un código 204 (No Content) ya que la petición
        // es correcta (no hay fallo por parte del usuario)
        if (!tareas.Any())
            return NoContent();

        var tareaDtos = tareas
            .Select(t => new TareaDTO(
                t.Id,
                t.Title,
                t.Content,
                t.CreatedAt,
                t.IsCompleted,

                // Seleccionamos cada etiqueta de la tarea y creamos un DTO
                // NOTA: hay que convertir el resultado del `Select()` a una lista
                //       usando el método `ToList()` porque el DTO requiere el tipo `List`.
                //       Normalmente, se utilizan los tipos genéricos (`ICollection`, `IDictionary` o,
                //       el más común y abstracto, `IEnumerable`, como tipo de dato de parámetro.
                t.Etiquetas.Select(e => new EtiquetaDTO(e.Id, e.Name)).ToList())
            );

        return Ok(tareas);
    }

    [HttpPost("createtarea")]
    public async Task<ActionResult> CreateTareaAsync(CreateTareaRequestDTO request, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(request.Title))
            return BadRequest("Titulo vacio o nulo");

        if (string.IsNullOrEmpty(request.Content))
            return BadRequest("Contenido vacio o nulo");

        var buscaTarea = await _dbContext.Tareas.FirstOrDefaultAsync(t => t.Title == request.Title, token);
        if (buscaTarea != null)
            return BadRequest("Ya existe una tarea con ese titulo");

        var tarea = new Tarea()
        {
            Title = request.Title,
            Content = request.Content
        };

        await _dbContext.Tareas.AddAsync(tarea, token).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(tarea);
    }

    [HttpDelete]
    [Route("/api/gestortareas/deletetarea/{id}")]
    public async Task<ActionResult> DeleteTareaAsync(Guid Id, CancellationToken token = default)
    {
        var tarea = await _dbContext.Tareas.FirstOrDefaultAsync(t => t.Id == /*request.*/Id, token);
        if (tarea is null)
            return NotFound();

        _dbContext.Remove(tarea);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false); ;
        return Ok(tarea);
    }

    [HttpPut("updatetarea")]
    public async Task<ActionResult> UpdateTareaAsync(UpdateTareaRequestDTO request, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(request.NewContent))
            return BadRequest("Nuevo contenido vacio o nulo");

        var tarea = await _dbContext.Tareas.FirstOrDefaultAsync(t => t.Id == request.Id, token);
        if (tarea is null)
            return NotFound();

        tarea.Content = request.NewContent;
        _dbContext.Tareas.Update(tarea);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(tarea);
    }

    [HttpPost("completetarea")]
    public async Task<ActionResult> CompleteTarea(IdRequestDTO request, CancellationToken token = default)
    {
        return await SetCompletedStatusAsync(request.Id, DateTime.Now, token);
    }

    [HttpPost("setpendingtarea")]
    public async Task<ActionResult> SetPendingTareaAsync(IdRequestDTO request, CancellationToken token = default)
    {
        return await SetCompletedStatusAsync(request.Id, null, token);
    }

    private async Task<ActionResult> SetCompletedStatusAsync(Guid id, DateTime? dateTime, CancellationToken token = default)
    {
        var tarea = await _dbContext.Tareas.FirstOrDefaultAsync(x => x.Id == id, token);
        if (tarea is null)
            return NotFound();

        tarea.CompletedAt = dateTime;
        _dbContext.Tareas.Update(tarea);
        await _dbContext.SaveChangesAsync(token);
        return Ok(tarea);
    }


    #region Add & Remove Etiquetas
    [HttpPut("addetiquetatotarea")]
    public async Task<ActionResult> AddEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request, CancellationToken token = default)
    {
        return await AddRemoveEtiquetaToTareaAsync(request.IdTarea, request.IdEtiqueta, true, token);
    }

    [HttpPut("removetiquettarea")]
    public async Task<ActionResult> RemoveEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request, CancellationToken token = default)
    {
        return await AddRemoveEtiquetaToTareaAsync(request.IdTarea, request.IdEtiqueta, false, token);
    }

    private async Task<ActionResult> AddRemoveEtiquetaToTareaAsync(Guid IdTarea, Guid EtiquetaId, bool Add, CancellationToken token)
    {
        var tarea = await _dbContext.Tareas.Include(t => t.Etiquetas).FirstOrDefaultAsync(t => t.Id == IdTarea, token);
        if (tarea is null)
            return NotFound();

        var etiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Id == EtiquetaId, token);
        if (etiqueta is null)
            return NotFound();

        if (Add)
        {
            tarea.AddEtiqueta(etiqueta);
            _dbContext.Tareas.Update(tarea);

        }
        else
        {
            tarea.RemoveEtiqueta(etiqueta);
            _dbContext.Tareas.Update(tarea);

        }

        await _dbContext.SaveChangesAsync(token);
        return Ok(tarea);
    }
    #endregion
}
