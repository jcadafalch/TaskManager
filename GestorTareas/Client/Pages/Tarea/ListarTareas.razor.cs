using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Pages.Tarea;

/// <summary>
/// Página listar tareas
/// </summary>
public partial class ListarTareas
{
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;

    private TareaDTO[]? Tareas = default;

    /// <summary>
    /// Obtiene el listado de todas las tareas y lo asigna al atributo tareas
    /// </summary>
    private async Task CargarTareasAsync()
    {
        Tareas = await HttpTareas.ListAsync();
        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// Cada vez que se renderiza la pagina se ejecuta este metodo
    /// </summary>
    /// <param name="firstRender">Indica si es la primera vez que se renderiza</param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await CargarTareasAsync();
        }
    }

    /// <summary>
    /// Modifica el estado de la tarea
    /// </summary>
    /// <param name="isCompleted">Nuevo estado de la tarea; true=completada, false=no completada</param>
    /// <param name="id">Id de la tarea</param>
    protected async Task UpdateTareasCompleted(bool isCompleted, Guid id)
    {
        // Creamos el DTO y hacemos la petición al servidor
        var idRequest = new IdRequestDTO(id);
        var successResponse = isCompleted ? await HttpTareas.CompleteAsync(idRequest) : await HttpTareas.SetPendingAsync(idRequest);

        // Si no se ha podido añadir, mostramos un mensaje de error
        if (!successResponse)
        {
            SnackbarError();
            return;
        }

        // Si se ha añadido, volvemos a cargar las tareas con el nuevo estado
        await CargarTareasAsync();
    }

    /// <summary>
    /// Muestra una notificación indicando que la tarea no se ha podido completar
    /// </summary>
    protected void SnackbarError() => Snackbar.Add("Error en completar la tarea", Severity.Error);
}
