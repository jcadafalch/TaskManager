using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace GestorTareas.Client.Pages.Tarea;

public partial class ListarTareas
{
    [Inject] protected TareasHttpClient Http { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; }
    private TareaDTO[]? tareas = default;

    private async Task CargarTareasAsync()
    {
        tareas = await Http.GetListTareaAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await CargarTareasAsync();
        }
    }

    protected async Task UpdateTareasCompleted(bool isCompleted, Guid id)
    {
        var idRequest = new IdRequestDTO(id);
        HttpResponseMessage response = isCompleted ? await Http.GetCompleteTareaAsync(idRequest) : await Http.GetSetPendingTareaAsync(idRequest);

        if (!response.IsSuccessStatusCode)
        {
            SnackbarError();
            return;
        }

        await CargarTareasAsync();
    }

    protected void SnackbarError() => Snackbar.Add("Error en completar la tarea", Severity.Error);
}
