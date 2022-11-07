using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Tarea;

public partial class TareaComponent
{
    [Inject] IDialogService DialogService { get; set; }
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public TareaDTO Tarea { get; set; } = default!;

    [Parameter]
    public EtiquetaDTO[] Etiquetas { get; set; }

    [Parameter]
    public bool TareaStatus { get; set; } = default;

    [Parameter]
    public bool Delete { get; set; } = default!;

    [Parameter]
    public bool Modify { get; set; } = default!;

    [Parameter]
    public bool All { get; set; } = default!;

    [Parameter]
    public EventCallback<bool> OnStatusChanged { get; set; }

    [Parameter]
    public EventCallback<bool> OnTareaChanged { get; set; }


    protected async Task CheckBoxChanged(bool e)
    {
        TareaStatus = e;
        await InvokeAsync(StateHasChanged);
        await OnStatusChanged.InvokeAsync(TareaStatus);
    }

    protected async Task ChangeStatus()
    {
        TareaStatus = !TareaStatus;
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
            {"Modify", true }
        };

        var dialog = DialogService.Show<TareaDialog>("Editar", parameters);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            if (All)
            {
                await UpdatePage();
                return;
            }

            await InvokeAsync(StateHasChanged);
            NavigationManager.NavigateTo("/");

        }
    }

    #endregion

    #region Delete

    private async Task DeleteTareaAsync()
    {
        var parameters = new DialogParameters
       {
            {"Contenido", "" },
           {"Tarea", Tarea },
           {"Delete", true}
       };

        var dialog = DialogService.Show<TareaDialog>("¡AVISO!", parameters);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            if (All)
            {
                await UpdatePage();
                return;
            }
            await InvokeAsync(StateHasChanged);
            NavigationManager.NavigateTo("/");
        }

    }

    #endregion

    #region Etiquetas

    private async Task AddEtiquetaToTarea()
    {
        var parameters = new DialogParameters
        {
            {"Tarea", Tarea },
            {"Etiqueta", null },
            {"Add", true },
            {"Remove", false }
        };

        var options = new DialogOptions
        {
            CloseButton = true,
            DisableBackdropClick = false,

        };

        var dialog = DialogService.Show<AddRemoveEtiquetaDialog>("Añadir etiqueta a tarea", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {

            await UpdatePage();
            return;

        }
    }
    private async Task RemoveEtiquetaToTarea(MudChip chip)
    {
        EtiquetaDTO etiqueta = (EtiquetaDTO)chip.Tag;
        var parameters = new DialogParameters
        {
            {"Tarea", Tarea },
            {"Etiqueta", etiqueta },
            {"Add", false },
            {"Remove", true }
        };

        var options = new DialogOptions
        {
            CloseButton = true,
            DisableBackdropClick = false,

        };

        var dialog = DialogService.Show<AddRemoveEtiquetaDialog>("¡ATENCIÓN!", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {

            await UpdatePage();
            return;

        }
    }

    #endregion

    // Updates the current page
    private async Task UpdatePage()
    {
        await InvokeAsync(StateHasChanged);
        await OnTareaChanged.InvokeAsync(true);
    }
}

