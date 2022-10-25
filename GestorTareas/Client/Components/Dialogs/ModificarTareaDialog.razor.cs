using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace GestorTareas.Client.Components.Dialogs;

public partial class ModificarTareaDialog
{
    [Inject] protected HttpClient Http { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; }
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Parameter] public TareaDTO Tarea { get; set; }
    [Parameter] public string Contenido { get; set;}

    protected async Task Submit()
    {
        var updatetareadto = new UpdateTareaRequestDTO(Tarea.Id, Contenido);
        var response = await Http.PutAsJsonAsync("/api/gestortareas/updatetarea", updatetareadto);
        if (!response.IsSuccessStatusCode)
        {
            Snackbar.Add("Ha habido un error", Severity.Error);
            return;
        }
        MudDialog.Close(DialogResult.Ok(true));
    }
    void Cancel() => MudDialog.Cancel(); 
}
