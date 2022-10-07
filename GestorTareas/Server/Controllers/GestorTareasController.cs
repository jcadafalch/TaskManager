using GestorTareas.Dominio;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GestorTareas.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GestorTareasController : ControllerBase
{
    private readonly GestorTareasDbContext _dbContext;

    public GestorTareasController(GestorTareasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Tareas
    //FUNCIONA
    [HttpGet("listtarea")]
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
                t.Etiquetas.Select(e => new TareaEtiquetaDTO(t.Id, e.Id)).ToList())
            );

        return Ok(tareas);
    }

    // FUNCIONA
    /* CreateTareaRequestDTO request, CancellationToken token = default*/
    [HttpPost("createtarea")]
    public async Task<ActionResult> CreateTareaAsync(string Title, string Content, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(/*request.*/Title))
            return BadRequest("Titulo vacio o nulo");

        if (string.IsNullOrEmpty(/*request.*/Content))
            return BadRequest("Contenido vacio o nulo");

        var buscaTarea = await _dbContext.Tareas.FirstOrDefaultAsync(t => t.Title == /*request.*/Title, token);
        if (buscaTarea != null)
            return BadRequest("Ya existe una tarea con ese titulo");

        var tarea = new Tarea()
        {
            Title = /*request.*/Title,
            Content = /*request.*/Content
        };

        await _dbContext.Tareas.AddAsync(tarea, token).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(tarea);
    }

    // FUNCIONA
    /* DeleteTareaRequestDTO request, CancellationToken token = default */
    [HttpDelete("deletetarea")]
    public async Task<ActionResult> DeleteTareaAsync(Guid Id, CancellationToken token = default)
    {
        var tarea = await _dbContext.Tareas.FirstOrDefaultAsync(t => t.Id == /*request.*/Id, token);
        if (tarea is null)
            return NotFound();

        _dbContext.Remove(tarea);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false); ;
        return Ok(tarea);
    }

    // FUNCIONA
    // UpdateTareaRequest request
    [HttpPut(("updatetarea"))]
    public async Task<ActionResult> UpdateTareaAsync(Guid Id, string NewContent, CancellationToken token)
    {
        if (string.IsNullOrEmpty(/*request.*/NewContent))
            return BadRequest("Nuevo contenido vacio o nulo");

        var tarea = await _dbContext.Tareas.FirstOrDefaultAsync(t => t.Id == /*request.*/Id, token);
        if (tarea is null)
            return NotFound();

        tarea.Content = /*request.*/NewContent;
        _dbContext.Tareas.Update(tarea);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(tarea);
    }

    // FUNCIONA
    // CompleteTareaRequestDTO request
    [HttpPost("completetarea")]
    public async Task<ActionResult> CompleteTareaAsync(Guid Id, CancellationToken token = default)
    {
        return await SetCompletedStatusAsync(/*request.*/Id, DateTime.Now, token);
    }

    // FUNCIONA
    // SetPendingTareaRequestDTO request
    [HttpPost("setpendingtarea")]
    public async Task<ActionResult> SetPendingTareaAsync(Guid Id, CancellationToken token = default)
    {
        return await SetCompletedStatusAsync(/*request.*/Id, null, token);
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

    //* Etiquetas
    // FUNCTIONA
    [HttpGet("listetiqueta")]
    public async Task<ActionResult> ListEtiquetasAsync(
        CancellationToken cancellationToken = default
    )
    {

        var etiquetas = await _dbContext.Etiquetas
             .Include(t => t.Tareas)
            .ToListAsync(cancellationToken);

        if (!etiquetas.Any())
            return NoContent();

        var etiquetasDtos = etiquetas
            .Select(t => new EtiquetaDTO(
                t.Id,
                t.Name,
                t.Tareas.Select(e => new TareaEtiquetaDTO(t.Id, e.Id)).ToList())
            );

        return Ok(etiquetas);
    }

    //FUNCIONA
    /* CrearEtiquetaRequestDTO request, CancellationToken token = default*/
    [HttpPost("createetiqueta")]
    public async Task<ActionResult> CreateEtiquetaAsync(string Name, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(/*request.*/Name))
            return BadRequest("Titulo vacio o nulo");

        var buscaEtiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Name == /*request.*/Name, token);
        if (buscaEtiqueta != null)
            return BadRequest("Ya existe una etiqueta con ese nombre");

        var etiqueta = new Etiqueta()
        {
            Name = /*request.*/Name
        };

        await _dbContext.Etiquetas.AddAsync(etiqueta, token).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(etiqueta);
    }

    // DeleteEtiquetaRequestDTO request
    [HttpDelete("deleteetiqueta")]
    public async Task<ActionResult> DeleteEtiquetaAsync(Guid Id, CancellationToken token = default)
    {
        var etiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Id == /*request.*/Id, token);

        if (etiqueta is null)
            return NoContent();

        _dbContext.Remove(etiqueta);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(etiqueta);
    }

    //UpdateEtiquetaRequestDTO request
    [HttpPut("updateetiqueta")]
    public async Task<ActionResult> UpdateEtiqueta(Guid Id, string NewName, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(/*request.*/NewName))
            return BadRequest("Nuevo nombre vacio o nulo");

        var etiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Id == /*request.*/Id, token);
        if (etiqueta is null)
            return NotFound();

        etiqueta.Name = /*request.*/NewName;
        _dbContext.Etiquetas.Update(etiqueta);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(etiqueta);
    }
}
