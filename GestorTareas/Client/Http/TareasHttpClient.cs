using GestorTareas.Shared;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace GestorTareas.Client.Http;

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
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;
        return await _httpClient.GetFromJsonAsync<TareaDTO[]>("/api/tareas/list", token);
    }

    /// <summary>
    /// Crea una tarea.
    /// </summary>
    /// <param name="request">DTO con todos los atributos necesarios para la creación de una tarea.</param>
    /// <returns>true si la tarea se ha creado; sino, false.</returns>
    public async Task<bool> CreateAsync(CreateTareaRequestDTO request)
    {
        var tokenSource = new CancellationTokenSource();
        var response = await _httpClient.PostAsJsonAsync("/api/tareas/createtarea", request, tokenSource.Token);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Actualiza el contenido de una tarea.
    /// </summary>
    /// <param name="request">DTO con todos los atributos necesarios para actualizar una tarea.</param>
    /// <returns>true si la tarea se ha actualizado; sino, false.</returns>
    public async Task<bool> UpdateAsync(UpdateTareaRequestDTO request)
    {
        var tokenSource = new CancellationTokenSource();
        var response = await _httpClient.PutAsJsonAsync("/api/tareas/updatetarea", request, tokenSource.Token);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Elimina una tarea.
    /// </summary>
    /// <param name="request">DTO con todos los atributos necesarios para eliminar una tarea.</param>
    /// <returns>true si la tarea se ha eliminado; sino, false.</returns>
    public async Task<bool> DeleteAsync(IdRequestDTO request)
    {
        var tokenSource = new CancellationTokenSource();
        var response = await _httpClient.DeleteAsync($"/api/tareas/deletetarea/{request.Id}", tokenSource.Token);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Establece una tarea como completada.
    /// </summary>
    /// <param name="request">DTO con los atributos necesarios para completar una tarea.</param>
    /// <returns>true si la tarea se ha completado; sino, false.</returns>
    public async Task<bool> CompleteAsync(IdRequestDTO request)
    {
        var tokenSource = new CancellationTokenSource();
        var response = await _httpClient.PostAsJsonAsync("/api/tareas/completetarea", request, tokenSource.Token);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Establece una tarea como pendiente.
    /// </summary>
    /// <param name="request">DTO con los atributos necesarios para establecer una tarea como pendiente.</param>
    /// <returns>true si la tarea se ha establecido como pendiente; sino, false.</returns>
    public async Task<bool> SetPendingAsync(IdRequestDTO request)
    {
        var tokenSource = new CancellationTokenSource();
        var response = await _httpClient.PostAsJsonAsync("/api/tareas/setpendingtarea", request, tokenSource.Token);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Añade una etiqueta a una tarea.
    /// </summary>
    /// <param name="request">DTO con los valores necesarios para añadir una etiqueta a una tarea.</param>
    /// <returns>true si se ha añadido la etiqueta a la tarea; sino, false.</returns>
    public async Task<bool> AddEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request)
    {
        var tokenSource = new CancellationTokenSource();
        var response = await _httpClient.PutAsJsonAsync("/api/tareas/addetiquetatotarea", request, tokenSource.Token);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Retira una etiqueta a una tarea.
    /// </summary>
    /// <param name="request">DTO con los valores necesarios para retirar una etiqueta a una tarea.</param>
    /// <returns>true si se ha retirado la etiqueta a la tarea; sino, false.</returns>
    public async Task<bool> RemoveEtiquetaToTareaAsync(ManageEtiquetaTareaRequestDTO request)
    {
        var tokenSource = new CancellationTokenSource();
        var response = await _httpClient.PutAsJsonAsync("/api/tareas/removetiquettarea/", request, tokenSource.Token);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<string>> AddArchivoToTareaAsync(/*AddArchivoTareaRequestDTO request, */IList<IBrowserFile> files, TareaDTO tarea)
    {
        List<string> notUploadedFiles = new();
        foreach (var file in files)
        {
            using Stream s = file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 1024);
            using MemoryStream ms = new MemoryStream();
            await s.CopyToAsync(ms);
            byte[] fileBytes = ms.ToArray();
            string extn = new FileInfo(file.Name).Extension;

            var request = new AddArchivoTareaRequestDTO(tarea.Id, file.Name, fileBytes, extn);

            var tokenSource = new CancellationTokenSource();
            var response = await _httpClient.PutAsJsonAsync("/api/tareas/addarchivototarea", request, tokenSource.Token);


            if (!response.IsSuccessStatusCode)
                notUploadedFiles.Add(file.Name);

        }

        return notUploadedFiles;
    }
}
