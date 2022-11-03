﻿using GestorTareas.Dominio;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Server.Controllers;

/// <summary>
/// Gestiona las peticiones de etiquetas a la base de datos
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DbControllerEtiquetas : ControllerBase
{
    private readonly GestorTareasDbContext _dbContext;

    public DbControllerEtiquetas(GestorTareasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Obtiene un listado con todas las etiquetas
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>OkObjectResult si se ha obtenido el listado correctamente - NoContentResult si no existen etiquetas</returns>
    [HttpGet("listetiqueta")]
    public async Task<ActionResult> ListEtiquetasAsync(
        CancellationToken cancellationToken = default
    )
    {
        // Cargamos todas las etiquetas de la base de datos
        var etiquetas = await _dbContext.Etiquetas
             .Include(t => t.Tareas)
            .ToListAsync(cancellationToken);

        // Si no hay etiquetas, en vez de mostrar un código 400 (Bad Request),
        // se debería mostrar un código 204 (No Content) ya que la petición
        // es correcta (no hay fallo por parte del usuario)
        if (!etiquetas.Any())
            return NoContent();

        var etiquetasDtos = etiquetas
            .Select(e => new EtiquetaDTO(
                e.Id,
                e.Name
            ));

        return Ok(etiquetas);
    }

    /// <summary>
    /// Crea una etiqueta
    /// </summary>
    /// <param name="request">DTO con nombre de la etiqueta</param>
    /// <param name="token">Token de cancelación</param>
    /// <returns>OkObjectResult si la etiqueta se ha creado correctamente - BadRequestObjectResult si la etiqueta ya existe o si alguno de los campos es nulo</returns>
    [HttpPost("createetiqueta")]
    public async Task<ActionResult> CreateEtiquetaAsync(CreateEtiquetaRequestDTO request, CancellationToken token = default)
    {
        // Comprovamos que el parametro recibido no es nulo ni vacio
        if (string.IsNullOrEmpty(request.Name))
            return BadRequest("Titulo vacio o nulo");

        // Buscamos si existe una tarea con ese nombre y si existe devolvemos BadRequest
        var buscaEtiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Name == request.Name, token);
        if (buscaEtiqueta != null)
            return BadRequest("Ya existe una etiqueta con ese nombre");

        // Si no existe una etiqueta con ese nombre, la creamos en la base de datos
        var etiqueta = new Etiqueta()
        {
            Name = request.Name
        };

        await _dbContext.Etiquetas.AddAsync(etiqueta, token).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(etiqueta);
    }

    /// <summary>
    /// Elimina una etiqueta
    /// </summary>
    /// <param name="Id">Id de la tarea que se va a eliminar</param>
    /// <param name="token">Token de cancelación</param>
    /// <returns>OkObjectResult si se ha eliminado correctamente - NotFoundObjectResult si no se encuentra la etiqueta a eliminar</returns>
    [HttpDelete]
    [Route("/api/gestortareas/deleteetiqueta/{id}")]
    public async Task<ActionResult> DeleteEtiquetaAsync(Guid Id, CancellationToken token = default)
    {
        //Buscamos la etiqueta en la base de datos
        var etiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Id == Id, token);

        // Si no existe, devolvemos NotFound()
        if (etiqueta is null)
            return NotFound();

        // Si se encuentra la etiqueta, la eliminamos de la base de datos
        _dbContext.Remove(etiqueta);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(etiqueta);
    }

    /// <summary>
    /// Actualiza el nombre de la etiqueta
    /// </summary>
    /// <param name="request">DTO con el Id de la etiqueta a actualizar i el nuevo titulo de la etiqueta</param>
    /// <param name="token">Token de cancelación</param>
    /// <returns>OkObjectResult si se ha actualizado la etiqueta correctamente - BadRequestObjectResult si el nuevo titulo es nulo - NotFoundObjectResult si no se ha encontrado la etiqueta a actualizar></returns>
    [HttpPut("updateetiqueta")]
    public async Task<ActionResult> UpdateEtiqueta(UpdateEtiquetaRequestDTO request, CancellationToken token = default)
    {
        // Comprovamos que el parametro NewName no sea nulo ni vacio
        if (string.IsNullOrEmpty(request.NewName))
            return BadRequest("Nuevo nombre vacio o nulo");

        // Obtenemos de la base de datos la etiqueta con el Id que recibimos por el parametro request
        var etiqueta = await _dbContext.Etiquetas.FirstOrDefaultAsync(e => e.Id == request.Id, token);

        // Si la etiqueta es nula devolvemos NotFound
        if (etiqueta is null)
            return NotFound();

        // Canviamos el contido de la etiqueta i actualizamos la base de datos con los nuevos valores
        etiqueta.Name = request.NewName;
        _dbContext.Etiquetas.Update(etiqueta);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return Ok(etiqueta);
    }
}
