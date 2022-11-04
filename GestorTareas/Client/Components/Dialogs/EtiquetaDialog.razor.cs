using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;

public partial class EtiquetaDialog
{
    [Inject] protected EtiquetasHttpClient HttpEtiquetas { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; }
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }

    [Parameter] 
    public EtiquetaDTO Etiqueta { get; set; }

    [Parameter] 
    public string Contenido { get; set; }

    [Parameter]
    public bool Modify { get; set; } = default!;

    [Parameter]
    public bool Delete { get; set; } = default!;

    protected async Task Submit()
    {
        if(Modify)
        {
            var updateEtiquetaDto = new UpdateEtiquetaRequestDTO(Etiqueta.Id, Contenido);
            var successResponse = await HttpEtiquetas.UpdateAsync(updateEtiquetaDto);

            if (!successResponse)
            {
                Snackbar.Add("Ha habido un error en modificar la tarea", Severity.Error);
                return;
            }
            Snackbar.Add("La etiqueta " + Etiqueta.Name + " se ha modificado correctamente", Severity.Success);
        }

        if(Delete)
        {
            var deleteEtiquetaDto = new DeleteEtiquetaRequestDTO(Etiqueta.Id);
            var successResponse = await HttpEtiquetas.DeleteAsync(deleteEtiquetaDto);

            if (!successResponse)
            {
                Snackbar.Add("Ha habido un error en eliminar la etiqueta", Severity.Error);
                return;
            }

            Snackbar.Add("La etiqueta " + Etiqueta.Name + " se ha eliminado correctamente", Severity.Success);
        }

        MudDialog.Close(DialogResult.Ok(true));
    }
    void Cancel() => MudDialog.Cancel();
}
