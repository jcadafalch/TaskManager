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
    [HttpGet("listtarea")]
    public async Task<ActionResult> ListTareasAsync()
    {
        var tareas = await _dbContext.Tareas
            .Select(t => new TareaDTO(t.Id, t.Title, t.Content, t.CreatedAt, t.IsCompleted, (HashSet<EtiquetaDTO>)t.Etiquetas))
            .ToListAsync();
        if (!tareas.Any())
            return BadRequest("No existen tareas");

        return Ok(tareas);
    }

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

    [HttpDelete("deletetarea")]
    public async Task<ActionResult> DeleteTareaAsync(DeleteTareaRequestDTO request, CancellationToken token = default)
    {     
        var tarea = await _dbContext.Tareas.FirstOrDefaultAsync(t => t.Id == request.Id, token);
        if (tarea is null)
            return NotFound();

        _dbContext.Remove(tarea);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false); ;
        return Ok(tarea);
    }

    [HttpPut(("updatecontent"))]
    public async Task<ActionResult> UpdateContent(UpdateTareaRequest request, CancellationToken token)
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
    public async Task<ActionResult> CompleteTareaAsync(CompleteTareaRequestDTO request, CancellationToken token = default)
    {
        return await SetCompletedStatusAsync(request.Id, DateTime.Now, token);
    }

    [HttpPost("setpendingtarea")]
    public async Task<ActionResult> SetPendingTareaAsync(SetPendingTareaRequestDTO request, CancellationToken token = default)
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

    //* Etiquetas
}
