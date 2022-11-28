using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;

public partial class AddImage
{
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;
    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; } = default!;

    IList<IBrowserFile> files = new List<IBrowserFile>();

    private void UploadFiles(IBrowserFile file) => files.Add(file);

    private void Upload()
    {
        // Upload files here
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
        Snackbar.Add("TODO: Upload your files!", Severity.Normal);

        MudDialog.Close(DialogResult.Ok(true));
        
    }

    /// <summary>
    /// Retira un fichero de la lista de ficheros a subir
    /// </summary>
    /// <param name="file">Fichero a retirar</param>
    private void RemoveFile(IBrowserFile file) => files.Remove(file);

    /// <summary>
    /// Cierra el diálogo sin hacer ninguna acción.
    /// </summary>
    void Cancel() => MudDialog.Cancel();
}
