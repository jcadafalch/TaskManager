﻿using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;

public partial class EtiquetaDialog
{
    [Inject] protected EtiquetasHttpClient Http { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; }
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Parameter] public EtiquetaDTO Etiqueta { get; set; }
    [Parameter] public string Contenido { get; set; }
    [Parameter] public string Action { get; set; }

    protected async Task Submit()
    {
        if(Action == "Modify")
        {
            var updateEtiquetaDto = new UpdateEtiquetaRequestDTO(Etiqueta.Id, Contenido);
            var response = await Http.GetUpdateEtiquetaAsync(updateEtiquetaDto);

            if (!response.IsSuccessStatusCode)
            {
                Snackbar.Add("Ha habido un error en modificar la tarea", Severity.Error);
                return;
            }
            Snackbar.Add("La etiqueta " + Etiqueta.Name + " se ha modificado correctamente", Severity.Success);
        }

        if(Action == "Delete")
        {
            var deleteEtiquetaDto = new DeleteEtiquetaRequestDTO(Etiqueta.Id);
            var response = await Http.GetDeleteEtiquetaAsync(deleteEtiquetaDto);

            if (!response.IsSuccessStatusCode)
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
