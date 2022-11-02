using GestorTareas.Dominio;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DbControllerEtiquetas : ControllerBase
{
    private readonly GestorTareasDbContext _dbContext;

    public DbControllerEtiquetas(GestorTareasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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
            .Select(e => new EtiquetaDTO(
                e.Id,
                e.Name
            ));

        return Ok(etiquetas);
    }

    [HttpPost("createetiqueta")]
    public async Task<ActionResult> CreateEtiquetaAsync(CreateEtiquetaRequestDTO request, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(request.Name))
            return BadRequest("Titulo vacio o nulo");

        var buscaEtiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Name == request.Name, token);
        if (buscaEtiqueta != null)
            return BadRequest("Ya existe una etiqueta con ese nombre");

        var etiqueta = new Etiqueta()
        {
            Name = request.Name
        };

        await _dbContext.Etiquetas.AddAsync(etiqueta, token).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(etiqueta);
    }

    [HttpDelete]
    [Route("/api/gestortareas/deleteetiqueta/{id}")]
    public async Task<ActionResult> DeleteEtiquetaAsync(Guid Id, CancellationToken token = default)
    {
        var etiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Id == Id, token);

        if (etiqueta is null)
            return NoContent();

        _dbContext.Remove(etiqueta);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(etiqueta);
    }

    [HttpPut("updateetiqueta")]
    public async Task<ActionResult> UpdateEtiqueta(UpdateEtiquetaRequestDTO request, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(request.NewName))
            return BadRequest("Nuevo nombre vacio o nulo");

        var etiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Id == request.Id, token);
        if (etiqueta is null)
            return NotFound();

        etiqueta.Name = request.NewName;
        _dbContext.Etiquetas.Update(etiqueta);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(etiqueta);
    }
}
