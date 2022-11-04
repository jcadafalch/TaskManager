using GestorTareas.Shared;
using System.Net.Http.Json;

namespace GestorTareas.Client.Models;

public class EtiquetasHttpClient
{
    private readonly HttpClient _httpClient;

    public EtiquetasHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EtiquetaDTO[]> ListAsync()
    {
        return await _httpClient.GetFromJsonAsync<EtiquetaDTO[]>("/api/etiquetas/listetiqueta");
    }

    public async Task<bool> CreateAsync(CreateEtiquetaRequestDTO request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/etiquetas/createetiqueta", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(UpdateEtiquetaRequestDTO request)
    {
        var response = await _httpClient.PutAsJsonAsync("/api/etiquetas/updateetiqueta", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(DeleteEtiquetaRequestDTO request)
    {
        var response = await _httpClient.DeleteAsync("/api/etiquetas/deleteetiqueta/" + request.Id);
        return response.IsSuccessStatusCode;
    }

}
