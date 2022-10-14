using GestorTareas.Client.Components;
using GestorTareas.Shared;
using MudBlazor;
using System.Net.Http.Json;

namespace GestorTareas.Client.Pages.Tarea;

public partial class EliminarTarea
{
    private IDialogService _dialogService;
    private HttpClient Http;
    private TareaDTO[]? Tareas { get; set; }
    private TareaDTO? Tarea { get; set; }
    
    private async Task CargarTareasAsync()
    {
        Tareas = await Http.GetFromJsonAsync<TareaDTO[]>("api/gestortareas/listtarea");
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await CargarTareasAsync();
        }
    }

    private async Task DeleteTask()
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", "Estas seguro que qieres eliminar la tarea? \nEste proceso no se podrá deshacer");
        parameters.Add("ButtonText", "Eliminar");
        parameters.Add("ColorButton", Color.Error);

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
        var delete = await Http.PostAsJsonAsync("api/gestortareas/deletetarea", Tarea.Id);
    }
}
