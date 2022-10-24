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

    public async Task<TareaDTO[]> GetListTareaAsync()
    {
        return await _httpClient.GetFromJsonAsync<TareaDTO[]>("/api/gestortareas/list");
    }

    public async Task<HttpResponseMessage> GetCreateTareaAsync(CreateTareaRequestDTO request)
    {
        return await _httpClient.PostAsJsonAsync("/api/gestortareas/createtarea", request);
    }

    public async Task<HttpResponseMessage> GetUpdateTareaAsync(UpdateTareaRequestDTO request)
    {
        return await _httpClient.PostAsJsonAsync("/api/gestortareas/updatetarea", request);
    }

    public async Task<HttpResponseMessage> GetDeleteTareaAsync(IdRequestDTO request)
    {
        return await _httpClient.PostAsJsonAsync("api/gestortareas/deletetarea", request.Id);
    }

    public async Task<HttpResponseMessage> GetCompleteTareaAsync(IdRequestDTO request)
    {
        return await _httpClient.PostAsJsonAsync("/api/gestortareas/completetarea", request);
    }
    public async Task<HttpResponseMessage> GetSetPendingTareaAsync(IdRequestDTO request)
    {
        return await _httpClient.PostAsJsonAsync("/api/gestortareas/setpendingtarea", request);
    }

    public async Task<HttpResponseMessage> GetAddEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request)
    {
        return await _httpClient.PostAsJsonAsync("/api/gestortareas/addetiquetatotarea", request);
    }

    public async Task<HttpResponseMessage> GetRemoveEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request)
    {
        return await _httpClient.PostAsJsonAsync("/api/gestortareas/removetiquettarea", request);
    }
}
