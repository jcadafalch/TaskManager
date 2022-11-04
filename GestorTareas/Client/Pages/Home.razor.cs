using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Pages;

public partial  class Home
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

    protected async Task UpdatePage(bool update)
    {
        if (update)
            await CargarTareasAsync();
    }

    protected void SnackbarError() => Snackbar.Add("Error en completar la tarea", Severity.Error);

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
