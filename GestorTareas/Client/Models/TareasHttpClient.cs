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
        return await _httpClient.GetFromJsonAsync<TareaDTO[]>("/api/dbcontrollertareas/list");
    }

    public async Task<HttpResponseMessage> GetCreateTareaAsync(CreateTareaRequestDTO request)
    {
        return await _httpClient.PostAsJsonAsync("/api/dbcontrollertareas/createtarea", request);
    }

    public async Task<HttpResponseMessage> GetUpdateTareaAsync(UpdateTareaRequestDTO request)
    {
        return await _httpClient.PutAsJsonAsync("/api/dbcontrollertareas/updatetarea", request);
    }

    public async Task<HttpResponseMessage> GetDeleteTareaAsync(IdRequestDTO request)
    {
        return await _httpClient.DeleteAsync("/api/gestortareas/deletetarea/" + request.Id);
    }

    public async Task<HttpResponseMessage> GetCompleteTareaAsync(IdRequestDTO request)
    {
        return await _httpClient.PostAsJsonAsync("/api/dbcontrollertareas/completetarea", request);
    }
    public async Task<HttpResponseMessage> GetSetPendingTareaAsync(IdRequestDTO request)
    {
        return await _httpClient.PostAsJsonAsync("/api/dbcontrollertareas/setpendingtarea", request);
    }

    public async Task<HttpResponseMessage> GetAddEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request)
    {
        return await _httpClient.PutAsJsonAsync("/api/gestortareas/addetiquetatotarea", request);
    }

    public async Task<HttpResponseMessage> GetRemoveEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request)
    {
        return await _httpClient.PutAsJsonAsync("/api/gestortareas/removetiquettarea/", request);
    }
}
