using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;

public partial class TareaDialog
{
    [Inject] protected TareasHttpClient Http { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; }
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Parameter] public TareaDTO Tarea { get; set; }
    [Parameter] public string Contenido { get; set;}
    [Parameter] public string Action { get; set; }
    public string Titulo { get; set; }

    protected async Task Submit()
    {
        if(Action == "Modify")
        {
            var updatetareadto = new UpdateTareaRequestDTO(Tarea.Id, Contenido);
            var response = await Http.GetUpdateTareaAsync(updatetareadto);

            if (!response.IsSuccessStatusCode)
            {
                Snackbar.Add("Ha habido un error en modificar la tarea", Severity.Error);
                return;
            }

            Snackbar.Add("La Tarea " + Tarea.Title + " se ha modificado correctamente", Severity.Success);
        }

        if(Action == "Delete")
        {
            var deletetareadto = new IdRequestDTO(Tarea.Id);
            var response = await Http.GetDeleteTareaAsync(deletetareadto);

            if (!response.IsSuccessStatusCode)
            {
                Snackbar.Add("Ha habido un error en eliminar la tarea", Severity.Error);
                return;
            }

            Snackbar.Add("La tarea " + Tarea.Title + " se ha eliminado correctamente", Severity.Success);
        }

        if(Action == "Create")
        {
            var tareadto = new CreateTareaRequestDTO(Titulo, Contenido);
            var response = await Http.GetCreateTareaAsync(tareadto);

            if(!response.IsSuccessStatusCode)
            {
                Snackbar.Add("Ha habido un error en crear la tarea", Severity.Error);
                return;
            }

            Snackbar.Add("La tarea " + Titulo + " se ha creado correctamente", Severity.Success);
        }
        
        MudDialog.Close(DialogResult.Ok(true));
    }

    void Cancel() => MudDialog.Cancel(); 
}
