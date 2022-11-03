using GestorTareas.Dominio;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Server.Controllers;

/// <summary>
/// Gestiona las peticiones de tareas a la base de datos
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DbControllerTareas : ControllerBase
{
    private readonly GestorTareasDbContext _dbContext;

    public DbControllerTareas(GestorTareasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Obtiene un listado con todas las tareas
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>OkObjectResult si se ha obtenido el listado correctamente - NoContentResult si no existen tareas</returns>
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

    /// <summary>
    /// Crea una tarea
    /// </summary>
    /// <param name="request">DTO con el titulo y contenido de la nueva tarea</param>
    /// <param name="token">Token de cancelación</param>
    /// <returns>OkObjectResult si la tarea se ha creado correctamente - BadRequestObjectResult si la tarea ya existe o si alguno de los campos es nulo</returns>
    [HttpPost("createtarea")]
    public async Task<ActionResult> CreateTareaAsync(CreateTareaRequestDTO request, CancellationToken token = default)
    {
        // Comprovamos que los parametros recibidos no sean nulos ni vacios
        if (string.IsNullOrEmpty(request.Title))
            return BadRequest("Titulo vacio o nulo");

        if (string.IsNullOrEmpty(request.Content))
            return BadRequest("Contenido vacio o nulo");

        // Buscamos si existe una tarea con ese nombre y si existe devolvemos BadRequest
        var buscaTarea = await _dbContext.Tareas.FirstOrDefaultAsync(t => t.Title == request.Title, token);
        if (buscaTarea != null)
            return BadRequest("Ya existe una tarea con ese titulo");

        // Si no existe una tarea con ese nombre, creamos la tarea en la base de datos
        var tarea = new Tarea()
        {
            Title = request.Title,
            Content = request.Content
        };

        await _dbContext.Tareas.AddAsync(tarea, token);
        await _dbContext.SaveChangesAsync(token);
        return Ok(tarea);
    }
    /// <summary>
    /// Elimina una tarea
    /// </summary>
    /// <param name="Id">Id de la tarea que se va a eliminar</param>
    /// <param name="token">Token de cancelación</param>
    /// <returns>OkObjectResult si se ha eliminado correctamente - NotFoundObjectResult si no se encuentra la tarea a eliminar</returns>
    [HttpDelete]
    [Route("/api/gestortareas/deletetarea/{id}")]
    public async Task<ActionResult> DeleteTareaAsync(Guid Id, CancellationToken token = default)
    {
        // Buscamos la tarea en la base de datos
        var tarea = await _dbContext.Tareas.FirstOrDefaultAsync(t => t.Id == Id, token);

        // Si no existe devolvemos NotFound
        if (tarea is null)
            return NotFound();

        // Si se encuentra la tarea, la eliminamos de la base de datos
        _dbContext.Remove(tarea);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false); ;
        return Ok(tarea);
    }

    /// <summary>
    /// Actualiza el contenido de la tarea
    /// </summary>
    /// <param name="request">DTO con el Id de la tarea a actualizar i el nuevo contenido de la tarea</param>
    /// <param name="token">Token de cancelacion</param>
    /// <returns>OkObjectResult si se ha actualizado la tarea correctamente - BadRequestObjectResult si el nuevo contenido es nulo - NotFoundObjectResult si no se ha encontrado la tarea a actualizar</returns>
    [HttpPut("updatetarea")]
    public async Task<ActionResult> UpdateTareaAsync(UpdateTareaRequestDTO request, CancellationToken token = default)
    {
        // Comprovamos que el parametro NewContent no sea nulo ni vacio
        if (string.IsNullOrEmpty(request.NewContent))
            return BadRequest("Nuevo contenido vacio o nulo");

        // Obtenemos de la base de datos la tarea con el Id que recibimos por el parametro request
        var tarea = await _dbContext.Tareas.FirstOrDefaultAsync(t => t.Id == request.Id, token);

        // Si la tarea es nula devolvemos NotFound
        if (tarea is null)
            return NotFound();

        // Canviamos el contido de la tarea y actualizamos la base de datos con los nuevos valores
        tarea.Content = request.NewContent;
        _dbContext.Tareas.Update(tarea);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(tarea);
    }

    /// <summary>
    /// Establece la tarea como completada
    /// </summary>
    /// <param name="request">DTO con el Id de la tarea</param>
    /// <param name="token">Token de cancelación</param>
    /// <returns>OkObjectResult si se ha completado la tarea - NotFoundObjectResult si no se ha encontrado la tarea</returns>
    [HttpPost("completetarea")]
    public async Task<ActionResult> CompleteTarea(IdRequestDTO request, CancellationToken token = default)
    {
        return await SetCompletedStatusAsync(request.Id, DateTime.Now, token);
    }

    /// <summary>
    /// Establece la tarea como pendiente
    /// </summary>
    /// <param name="request">DTO con el Id de la tarea</param>
    /// <param name="token">Token de cancelación</param>
    /// <returns>OkObjectResult si se ha establecido la tarea como pendiente - NotFoundObjectResult si no se ha encontrado la tarea</returns>
    [HttpPost("setpendingtarea")]
    public async Task<ActionResult> SetPendingTareaAsync(IdRequestDTO request, CancellationToken token = default)
    {
        return await SetCompletedStatusAsync(request.Id, null, token);
    }

    /// <summary>
    /// Establece la tarea como pendiente o completada
    /// </summary>
    /// <param name="id">Id de la tarea</param>
    /// <param name="dateTime">Null si se descompleta o not null si se completa</param>
    /// <param name="token">Token de cancelación</param>
    /// <returns> OkObjectResult si se ha realizado la acción - NotFoundObjectResult si no se ha encontrado la tarea</returns>
    private async Task<ActionResult> SetCompletedStatusAsync(Guid id, DateTime? dateTime, CancellationToken token = default)
    {
        // Obtenemos la tarea de la base de datos
        var tarea = await _dbContext.Tareas.FirstOrDefaultAsync(x => x.Id == id, token);

        // Si no existe la tarea, devolvemos NotFound
        if (tarea is null)
            return NotFound();

        // Cambiamos el estado de la tarea y actualizamos la base de datos con los nuevos valores.
        tarea.CompletedAt = dateTime;
        _dbContext.Tareas.Update(tarea);
        await _dbContext.SaveChangesAsync(token);
        return Ok(tarea);
    }


    #region Add & Remove Etiquetas

    /// <summary>
    /// Añade una etiqueta a una tarea
    /// </summary>
    /// <param name="request">DTO con el Id de la tarea, el Id de la etiqueta</param>
    /// <param name="token">Token de cancelación</param>
    /// <returns>OkObjectResult si se ha añadido la etiqueta a la tarea - NotFoundObjectResult si no se ha encontrado la tarea o etiqueta</returns>
    [HttpPut("addetiquetatotarea")]
    public async Task<ActionResult> AddEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request, CancellationToken token = default)
    {
        return await AddRemoveEtiquetaToTareaAsync(request.IdTarea, request.IdEtiqueta, true, token);
    }

    /// <summary>
    /// Elimina una etiqueta a una tarea
    /// </summary>
    /// <param name="request">DTO con el Id de la tarea, el Id de la etiqueta</param>
    /// <param name="token">Token de cancelación</param>
    /// <returns>OkObjectResult si se ha eliminado la etiqueta de la tarea - NotFoundObjectResult si no se ha encontrado la tarea o etiqueta</returns>
    [HttpPut("removetiquettarea")]
    public async Task<ActionResult> RemoveEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request, CancellationToken token = default)
    {
        return await AddRemoveEtiquetaToTareaAsync(request.IdTarea, request.IdEtiqueta, false, token);
    }

    /// <summary>
    /// Añade o elimina una etiqueta de una tarea
    /// </summary>
    /// <param name="IdTarea">Id de la tarea</param>
    /// <param name="EtiquetaId">Id de la etiqueta</param>
    /// <param name="Add">Bool para saber que acción se va hacer, true si se añade o false si se elimina</param>
    /// <param name="token">Token de cancelación</param>
    /// <returns>OkObjectResult si se ha eliminado/añadido etiqueta a la tarea; NotFoundObjectResult si no se ha encontrado la tarea o la etiqueta</returns>
    private async Task<ActionResult> AddRemoveEtiquetaToTareaAsync(Guid IdTarea, Guid EtiquetaId, bool Add, CancellationToken token)
    {
        // Obtenemos la tarea de la base de datos
        var tarea = await _dbContext.Tareas.Include(t => t.Etiquetas).FirstOrDefaultAsync(t => t.Id == IdTarea, token);

        // Si no existe la tarea, devolvemos NotFound
        if (tarea is null)
            return NotFound();

        // Obtenemos la etiqueta de la base de datos
        var etiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Id == EtiquetaId, token);

        // Si no existe la etiqueta, devolvemos NotFound
        if (etiqueta is null)
            return NotFound();

        // Si el parametro Add es true añadimos la etiqueta a la tarea, en caso contrario la quitamos
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

        // Actualitzamos la base de datos
        await _dbContext.SaveChangesAsync(token);
        return Ok(tarea);
    }
    #endregion
}
