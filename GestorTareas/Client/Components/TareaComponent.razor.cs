using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components;

public partial class TareaComponent
{
    [Inject] protected TareasHttpClient Http { get; set; } = default!;
    [Inject] IDialogService DialogService { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public TareaDTO Tarea { get; set; } = default!;

    [Parameter]
    public bool TareaStatus { get; set; } = default;

    [Parameter]
    public string? Action { get; set; }

    //public string? Action = "Modify";

    [Parameter]
    public EventCallback<bool> OnStatusChanged { get; set; }

    MudMessageBox? mbox { get; set; }
    string state = "Message box hasn't been opened yet";


    protected async Task CheckBoxChanged(bool e)
    {
        TareaStatus = e;
        await InvokeAsync(StateHasChanged);
        await OnStatusChanged.InvokeAsync(TareaStatus);
    }

    #region Modify
    protected async Task ModifyTarea()
    {
        var parameters = new DialogParameters
        {
            { "Contenido", Tarea.Content },
            { "Tarea", Tarea }
        };

        var options = new DialogOptions()
        {
            DisableBackdropClick = true
        };

        var dialog = DialogService.Show<ModificarTareaDialog>("Editar", parameters, options);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            await InvokeAsync(StateHasChanged);
            NavigationManager.NavigateTo("/");
         
        }
    }

    #endregion

    #region Delte
    private async void ConfirmActionDeleteAsync()
    {
        bool? result = await mbox.Show();
        state = result == null ? "Cancelled" : "Deleted";

        if (result is not null)
            await DeleteTareaAsync();

        StateHasChanged();
    }

    private async Task DeleteTareaAsync()
    {
        if (Tarea is null)
        {
            return;
        }

        IdRequestDTO idrequest = new IdRequestDTO(Tarea.Id);
        var response = await Http.GetDeleteTareaAsync(idrequest);

        if (!response.IsSuccessStatusCode)
        {
            Snackbar.Add("Ha habido un error en intentar eliminar la tarea", Severity.Error);
            return;
        }

        await InvokeAsync(StateHasChanged);
        NavigationManager.NavigateTo("/");
    }

    #endregion
}

