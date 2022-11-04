using GestorTareas.Shared;
using System.Net.Http.Json;

namespace GestorTareas.Client.Models;

public class TareasHttpClient
{
    private readonly HttpClient _httpClient;

    public TareasHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TareaDTO[]> ListAsync()
    {
        return await _httpClient.GetFromJsonAsync<TareaDTO[]>("/api/tareas/list");
    }

    public async Task<bool> CreateAsync(CreateTareaRequestDTO request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/tareas/createtarea", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(UpdateTareaRequestDTO request)
    {
        var response = await _httpClient.PutAsJsonAsync("/api/tareas/updatetarea", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(IdRequestDTO request)
    {
        var response = await _httpClient.DeleteAsync($"/api/tareas/deletetarea/{request.Id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CompleteAsync(IdRequestDTO request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/tareas/completetarea", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetPendingAsync(IdRequestDTO request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/tareas/setpendingtarea", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AddEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request)
    {
        var response = await _httpClient.PutAsJsonAsync("/api/tareas/addetiquetatotarea", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request)
    {
        var response = await _httpClient.PutAsJsonAsync("/api/tareas/removetiquettarea/", request);
        return response.IsSuccessStatusCode;
    }
}
