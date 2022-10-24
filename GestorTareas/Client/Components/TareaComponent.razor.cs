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
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public TareaDTO Tarea { get; set; } = default!;

    [Parameter]
    public bool TareaStatus { get; set; } = default;

    [Parameter]
    public string? Action { get; set; }

    //public string? Action = "All";

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
    protected void ModifyTarea()
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

        DialogService.Show<ModificarTareaDialog>("Editar", parameters);
    }

    #endregion

    #region Delte
    //private async Task ConfirmAction()
    //{

    //    var parameters = new DialogParameters();
    //    parameters.Add("ContentText", "Estas seguro que qieres eliminar la tarea? \nEste proceso no se podrá deshacer");
    //    parameters.Add("ButtonText", "Eliminar");
    //    parameters.Add("Color", Color.Error);

    //    var options = new DialogOptions()
    //    {
    //        CloseButton = true,
    //        MaxWidth = MaxWidth.ExtraSmall,
    //        DisableBackdropClick = true
    //    };

    //    var dialogResult = Dialog.Show<MudDialogComponent>("Eliminar Tarea", parameters, options);
    //    var result = await dialogResult.Result;
    //    if (!result.Cancelled && bool.TryParse(result.Data.ToString(), out bool resultbool)) DeleteTarea();
    //}

    private async void OnButtonClicked()
    {
        bool? result = await DialogService.ShowMessageBox(
            "Warning",
            "Deleting can not be undone!",
            yesText: "Delete!", cancelText: "Cancel");
        state = result == null ? "Cancelled" : "Deleted!";
        StateHasChanged();

        /*Console.WriteLine("Boto eliminar");

         bool? result = await mbox.Show();
         state = result == null ? "Cancelled" : "Deleted!";

         Console.WriteLine("State = " + state);
         StateHasChanged();*/
    }

    //private async Task DeleteTareaAsync()
    //{
    //    if (Tarea is null)
    //    {
    //        return;
    //    }
    //    IdRequestDTO idrequest = new IdRequestDTO(Tarea.Id);
    //    var response = await Http.GetDeleteTareaAsync(idrequest);

    //    if (!response.IsSuccessStatusCode)
    //    {
    //        Snackbar.Add("Ha habido un error en intentar eliminar la tarea", Severity.Error);
    //        return;
    //    }

    //    await InvokeAsync(StateHasChanged);
    //    NavigationManager.NavigateTo("/");
    //}

    #endregion
}

