﻿using GestorTareas.Shared;
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
        return await _httpClient.GetFromJsonAsync<EtiquetaDTO[]>("/api/gestortareas/listetiqueta");
    }

    public async Task<HttpResponseMessage> GetCreateEtiquetaAsync(CreateEtiquetaRequestDTO request)
    {
        return await _httpClient.PostAsJsonAsync("/api/gestortareas/createetiqueta", request);
    }

    public async Task<HttpResponseMessage> GetUpdateEtiquetaAsync(UpdateEtiquetaRequestDTO request)
    {
        return await _httpClient.PutAsJsonAsync("/api/gestortareas/updateetiqueta", request);
    }

    public async Task<HttpResponseMessage> GetDeleteEtiquetaAsync(DeleteEtiquetaRequestDTO request)
    {
        return await _httpClient.DeleteAsync("/api/gestortareas/deleteetiqueta/" + request.Id);
    }

}