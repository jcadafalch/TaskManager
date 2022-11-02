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

    public async Task<EtiquetaDTO[]> GetListEtiquetaAsync()
    {
        return await _httpClient.GetFromJsonAsync<EtiquetaDTO[]>("/api/dbcontrolleretiquetas/listetiqueta");
    }

    public async Task<HttpResponseMessage> GetCreateEtiquetaAsync(CreateEtiquetaRequestDTO request)
    {
        return await _httpClient.PostAsJsonAsync("/api/dbcontrolleretiquetas/createetiqueta", request);
    }

    public async Task<HttpResponseMessage> GetUpdateEtiquetaAsync(UpdateEtiquetaRequestDTO request)
    {
        return await _httpClient.PutAsJsonAsync("/api/dbcontrolleretiquetas/updateetiqueta", request);
    }

    public async Task<HttpResponseMessage> GetDeleteEtiquetaAsync(DeleteEtiquetaRequestDTO request)
    {
        return await _httpClient.DeleteAsync("/api/gestortareas/deleteetiqueta/" + request.Id);
    }

}
