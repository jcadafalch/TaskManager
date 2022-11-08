using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Pages;

/// <summary>
/// Pagina prinipal de la web
/// </summary>
public partial class Home
{
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    [Inject] protected EtiquetasHttpClient HttpEtiquetas { get; set; }
    [Inject] protected IDialogService DialogService { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }

    private TareaDTO[]? tareas = default;
    private EtiquetaDTO[]? etiquetas = default!;

    private async Task CargarTareasAsync()
    {
        tareas = await HttpTareas.ListAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task CargarEtiquetasAsync()
    {
        etiquetas = await HttpEtiquetas.ListAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await CargarTareasAsync();
            await CargarEtiquetasAsync();
        }
    }

    /// <summary>
    /// Modifica el estado de la tarea
    /// </summary>
    /// <param name="isCompleted">Nuevo estado de la tarea; true=completada, false=no completada</param>
    /// <param name="id">Id de la tarea</param>
    protected async Task UpdateTareasCompleted(bool isCompleted, Guid id)
    {
        var idRequest = new IdRequestDTO(id);
        var successResponse = isCompleted ? await HttpTareas.CompleteAsync(idRequest) : await HttpTareas.SetPendingAsync(idRequest);

        if (!successResponse)
        {
            SnackbarError();
            return;
        }
        await CargarTareasAsync();
    }

    /// <summary>
    /// Actualiza la pagina
    /// </summary>
    /// <param name="update">Booleano para saber si hay que actualizar no</param>
    protected async Task UpdatePage(bool update)
    {
        if (update)
            await CargarTareasAsync();
    }

    /// <summary>
    /// Muestra una notificación indicando que la tarea no se ha podido completar
    /// </summary>
    protected void SnackbarError() => Snackbar.Add("Error en completar la tarea", Severity.Error);

    /// <summary>
    /// Abre un popup para la creación de una nueva tarea
    /// </summary>
    private async Task CreateTarea()
    {
        var parameters = new DialogParameters
        {
            {"Create",  true }
        };

        var options = new DialogOptions()
        {
            DisableBackdropClick = true
    };
        var dialog = DialogService.Show<TareaDialog>("Crear Tarea", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            await CargarTareasAsync();
            return;
        }
        
    }

}
