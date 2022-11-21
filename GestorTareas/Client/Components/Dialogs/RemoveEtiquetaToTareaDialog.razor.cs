using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;

/// <summary>
/// Diálogo para retirar una etiqueta de una tarea
/// </summary>
public partial class RemoveEtiquetaToTareaDialog
{
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public TareaDTO Tarea { get; set; } = default!;
    [Parameter]
    public EtiquetaDTO Etiqueta { get; set; } = default!;

    /// <summary>
    /// Retira la etiqueta de la tarea
    /// </summary>
    private async Task RemoveEtiquetaAsync()
    {
        // Creamos el DTO i hacemos la petición al servidor
        var removeEtiquetaTarea = new ManageEtiquetaTareaRequestDTO(Tarea.Id, Etiqueta.Id);
        var successResponse = await HttpTareas.RemoveEtiquetaToTareaAsync(removeEtiquetaTarea);

        // Si no se ha podido retirar, mostramos un mensaje de error
        if (!successResponse)
        {
            Snackbar.Add($"Ha habido un error en retirar la etiqueta {Etiqueta.Name} a la tarea {Tarea.Title}", Severity.Error);
            return;
        }

        // Si se ha retirado, notificamos al usuario
        Snackbar.Add($"Se ha retirado la etiqueta {Etiqueta.Name} a la tarea {Tarea.Title}", Severity.Success);

        MudDialog.Close(DialogResult.Ok(true));
    }
}
