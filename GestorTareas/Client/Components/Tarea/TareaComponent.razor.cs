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
    public string? Action { get; set; }

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
            {"Action", "Modify" }
        };

        var dialog = DialogService.Show<TareaDialog>("Editar", parameters);
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            if (Action == "All")
            {
                await UpdatePage();
                return;
            }

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

        var dialog = DialogService.Show<TareaDialog>("¡AVISO!", parameters);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            if (Action == "All")
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
            {"LabelContent", "Selecciona la etiqueta que quieres añadir" },
            {"Tarea", Tarea }
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
        var removeEtiquetaTarea = new ManageEtiquetaTareaRequestDTO(Tarea.Id, etiqueta.Id);
        var successResponse = await HttpTareas.RemoveEtiquetaToTareaAsync(removeEtiquetaTarea);

        if (!successResponse)
        {
            Snackbar.Add("Ha habido un error en retirar la etiqueta " + etiqueta.Name + " a la tarea " + Tarea.Title, Severity.Error);
            return;
        }

        await UpdatePage();
        return;
    }

    #endregion

    // Updates the current page
    private async Task UpdatePage()
    {
        await InvokeAsync(StateHasChanged);
        await OnTareaChanged.InvokeAsync(true);
    }
}

