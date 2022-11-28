using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;

public partial class AddImage
{
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;
    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; } = default!;

    IList<IBrowserFile> Files = new List<IBrowserFile>();

    private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full z-10";
    private string DragClass = DefaultDragClass;

    private void UploadFiles(IBrowserFile file) => Files.Add(file);

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
    private void RemoveFile(IBrowserFile file) => Files.Remove(file);

    private void OnInputFileChanged(InputFileChangeEventArgs e)
    {
        ClearDragClass();
        var files = e.GetMultipleFiles();
        foreach(var file in files)
        {
            Files.Add(file);
        }
    }

    private void SetDragClass()
    {
        DragClass = $"{DefaultDragClass} mud-border-primary";
    }

    private void ClearDragClass()
    {
        DragClass= DefaultDragClass;
    }

    /// <summary>
    /// Cierra el diálogo sin hacer ninguna acción.
    /// </summary>
    void Cancel() => MudDialog.Cancel();
}
