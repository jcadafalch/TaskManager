﻿using GestorTareas.Shared;
using System.Net.Http.Json;

namespace GestorTareas.Client.Models;

/// <summary>
/// Gestiona las peticiones de tareas al servidor.
/// </summary>
public class TareasHttpClient
{
    private readonly HttpClient _httpClient;

    public TareasHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtiene un listado con todas las tareas.
    /// </summary>
    /// <returns>Listado de tareas.</returns>
    public async Task<TareaDTO[]?> ListAsync()
    {
        return await _httpClient.GetFromJsonAsync<TareaDTO[]>("/api/tareas/list");
    }

    /// <summary>
    /// Crea una tarea.
    /// </summary>
    /// <param name="request">DTO con todos los atributos necesarios para la creación de una tarea.</param>
    /// <returns>true si la tarea se ha creado; sino, false.</returns>
    public async Task<bool> CreateAsync(CreateTareaRequestDTO request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/tareas/createtarea", request);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Actualiza el contenido de una tarea.
    /// </summary>
    /// <param name="request">DTO con todos los atributos necesarios para actualizar una tarea.</param>
    /// <returns>true si la tarea se ha actualizado; sino, false.</returns>
    public async Task<bool> UpdateAsync(UpdateTareaRequestDTO request)
    {
        var response = await _httpClient.PutAsJsonAsync("/api/tareas/updatetarea", request);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Elimina una tarea.
    /// </summary>
    /// <param name="request">DTO con todos los atributos necesarios para eliminar una tarea.</param>
    /// <returns>true si la tarea se ha eliminado; sino, false.</returns>
    public async Task<bool> DeleteAsync(IdRequestDTO request)
    {
        var response = await _httpClient.DeleteAsync($"/api/tareas/deletetarea/{request.Id}");
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Establece una tarea como completada.
    /// </summary>
    /// <param name="request">DTO con los atributos necesarios para completar una tarea.</param>
    /// <returns>true si la tarea se ha completado; sino, false.</returns>
    public async Task<bool> CompleteAsync(IdRequestDTO request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/tareas/completetarea", request);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Establece una tarea como pendiente.
    /// </summary>
    /// <param name="request">DTO con los atributos necesarios para establecer una tarea como pendiente.</param>
    /// <returns>true si la tarea se ha establecido como pendiente; sino, false.</returns>
    public async Task<bool> SetPendingAsync(IdRequestDTO request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/tareas/setpendingtarea", request);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Añade una etiqueta a una tarea.
    /// </summary>
    /// <param name="request">DTO con los valores necesarios para añadir una etiqueta a una tarea.</param>
    /// <returns>true si se ha añadido la etiqueta a la tarea; sino, false.</returns>
    public async Task<bool> AddEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request)
    {
        var response = await _httpClient.PutAsJsonAsync("/api/tareas/addetiquetatotarea", request);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Retira una etiqueta a una tarea.
    /// </summary>
    /// <param name="request">DTO con los valores necesarios para retirar una etiqueta a una tarea.</param>
    /// <returns>true si se ha retirado la etiqueta a la tarea; sino, false.</returns>
    public async Task<bool> RemoveEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request)
    {
        var response = await _httpClient.PutAsJsonAsync("/api/tareas/removetiquettarea/", request);
        return response.IsSuccessStatusCode;
    }
}
