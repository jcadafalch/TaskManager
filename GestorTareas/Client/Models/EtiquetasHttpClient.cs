using GestorTareas.Shared;
using System.Net.Http.Json;

namespace GestorTareas.Client.Models;

/// <summary>
/// Gestiona las peticiones de etiquetas al servidor
/// </summary>
public class EtiquetasHttpClient
{
    private readonly HttpClient _httpClient;

    public EtiquetasHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtiene un listado con todas las etiquetas
    /// </summary>
    /// <returns>Listado de etiquetas</returns>
    public async Task<EtiquetaDTO[]> ListAsync()
    {
        return await _httpClient.GetFromJsonAsync<EtiquetaDTO[]>("/api/etiquetas/listetiqueta") ?? Array.Empty<EtiquetaDTO>();
    }

    /// <summary>
    /// Crea una etiqueta
    /// </summary>
    /// <param name="request">DTO con todos los atributos necesarios para la creación de una etiqueta.</param>
    /// <returns>true si la etiqueta se ha creado; sino, false.</returns>
    public async Task<bool> CreateAsync(CreateEtiquetaRequestDTO request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/etiquetas/createetiqueta", request);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Actualiza el nombre de una etiqueta
    /// </summary>
    /// <param name="request">DTO con todos los atributos necesarios para actualizar una etiqueta.</param>
    /// <returns>true si la etiqueta se ha actualizado; sino, false.</returns>
    public async Task<bool> UpdateAsync(UpdateEtiquetaRequestDTO request)
    {
        var response = await _httpClient.PutAsJsonAsync("/api/etiquetas/updateetiqueta", request);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Elimina una etiqueta
    /// </summary>
    /// <param name="request">DTO con todos los atributos necesarios para eliminar una etiqueta.</param>
    /// <returns>true si la etiqueta se ha eliminado; sino, false.</returns>
    public async Task<bool> DeleteAsync(DeleteEtiquetaRequestDTO request)
    {
        var response = await _httpClient.DeleteAsync("/api/etiquetas/deleteetiqueta/" + request.Id);
        return response.IsSuccessStatusCode;
    }

}
