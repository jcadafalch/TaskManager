using GestorTareas.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly GestorTareasDbContext _dbContext;

    public ApiController(GestorTareasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Tareas
    [HttpGet]
    public async Task<ActionResult> ListarTareas()
    {
        var tareas = await _dbContext.Tareas
            .Select(t => new TareaDTO(t.Id, t.Title, t.Content, t.CreatedAt, t.IsCompleted, (ICollection<EtiquetaDTO>?)t.Etiquetas))
            .ToListAsync();
        return Ok(tareas);
    }



    //* Etiquetas
}
