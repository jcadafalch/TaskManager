using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Tarea;

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
            { "Tarea", Tarea },
            {"Action", "Modify" }
        };

        var options = new DialogOptions()
        {
            DisableBackdropClick = true
        };

        var dialog = DialogService.Show<TareaDialog>("Editar", parameters, options);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            await InvokeAsync(StateHasChanged);
            NavigationManager.NavigateTo("/");

        }
    }

    #endregion

    #region Delte

    private async Task DeleteTareaAsync()
    {
        var parameters = new DialogParameters
       {
            {"Contenido", "" },
           {"Tarea", Tarea },
           {"Action", "Delete" }
       };

        var options = new DialogOptions()
        {
            DisableBackdropClick = true
        };

        var dialog = DialogService.Show<TareaDialog>("¡AVISO!", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            await InvokeAsync(StateHasChanged);
            NavigationManager.NavigateTo("/");
        }
        
    }

    #endregion
}

