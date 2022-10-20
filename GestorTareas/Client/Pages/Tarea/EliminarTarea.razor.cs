using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Pages.Tarea;

public partial class EliminarTarea
{
    [Inject] protected TareasHttpClient Http { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; }
    [Inject] protected IDialogService _dialogService { get; set; } = default!;
    private TareaDTO[]? Tareas { get; set; }
    private TareaDTO Tarea { get; set; }
    
    private async Task CargarTareasAsync()
    {
        Tareas = await Http.GetListTareaAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await CargarTareasAsync();
        }
    }

    private async Task ConfirmAction()
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", "Estas seguro que qieres eliminar la tarea? \nEste proceso no se podrá deshacer");
        parameters.Add("ButtonText", "Eliminar");
        parameters.Add("Color", Color.Error);

        var options = new DialogOptions()
        {
            CloseButton = true,
            MaxWidth = MaxWidth.ExtraSmall,
            DisableBackdropClick = true
        };

        var dialogResult = _dialogService.Show<MudDialogComponent>("Eliminar Tarea", parameters, options);
        var result = await dialogResult.Result;
        if (!result.Cancelled && bool.TryParse(result.Data.ToString(), out bool resultbool)) DeleteTarea();
    }

    private async Task DeleteTarea()
    {
        if(Tarea is null)
        {
            return;
        }
        IdRequestDTO idrequest = new IdRequestDTO(Tarea.Id);
        var response = await Http.GetDeleteTareaAsync(idrequest);

        if (!response.IsSuccessStatusCode)
        {
            //! Afegir algun popup avisant que alguna cosa ha pasat
            return;
        }
        
        await InvokeAsync(StateHasChanged);
        NavigationManager.NavigateTo("/");
    }
}
