using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
    /// Cierra el diálogo sin hacer ninguna acción.
    /// </summary>
    void Cancel() => MudDialog.Cancel();
}
