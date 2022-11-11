using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;

/// <summary>
/// Muestra un diálogo con las acciones de etiquetas
/// </summary>
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
    public bool IsModify { get; set; } = default!;

    [Parameter]
    public bool IsDelete { get; set; } = default!;

    /// <summary>
    /// Modifica la etiqueta
    /// </summary>
    private async Task ModifyAsync()
    {
        // Creamos el DTO y hacemos la petición al servidor
        var updateEtiquetaDto = new UpdateEtiquetaRequestDTO(Etiqueta.Id, Contenido);
        var successResponse = await HttpEtiquetas.UpdateAsync(updateEtiquetaDto);

        // Si no se ha podido modificar, mostramos un mensaje de error
        if (!successResponse)
        {
            Snackbar.Add("Ha habido un error en modificar la tarea", Severity.Error);
            return;
        }

        // Si se ha añadido, notificamos al usuario
        Snackbar.Add("La etiqueta " + Etiqueta.Name + " se ha modificado correctamente", Severity.Success);

        // Cerramos el diálogo
        MudDialog.Close(DialogResult.Ok(true));
    }

    /// <summary>
    /// Elimina una etiqueta
    /// </summary>
    private async Task DeleteAsync()
    {
        // Creamos el DTO y hacemos la petición al servidor
        var deleteEtiquetaDto = new DeleteEtiquetaRequestDTO(Etiqueta.Id);
        var successResponse = await HttpEtiquetas.DeleteAsync(deleteEtiquetaDto);

        // Si no se ha podido eliminar, mostramos un mensaje de error
        if (!successResponse)
        {
            Snackbar.Add("Ha habido un error en eliminar la etiqueta", Severity.Error);
            return;
        }

        // Si se ha eliminado, notificamos al usuario
        Snackbar.Add("La etiqueta " + Etiqueta.Name + " se ha eliminado correctamente", Severity.Success);

        // Cerramos el diálogo
        MudDialog.Close(DialogResult.Ok(true));
    }

    /// <summary>
    /// Cierra el dialogo sin hacer ninguna acción.
    /// </summary>
    void Cancel() => MudDialog.Cancel();
}
