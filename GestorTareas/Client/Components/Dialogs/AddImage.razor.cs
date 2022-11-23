using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;

public partial class AddImage
{
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;
    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; } = default!;

    private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full z-10";
    private string DragClass = DefaultDragClass;
    private List<string> fileNames = new List<string>();

    private void OnInputFileChanged(InputFileChangeEventArgs e)
    {
        ClearDragClass();
        var files = e.GetMultipleFiles();
        foreach(var file in files)
        {
            fileNames.Add(file.Name);
        }
    }

    private async Task Clear()
    {
        fileNames.Clear();
        ClearDragClass();
        await Task.Delay(100);
    }

    private void Upload()
    {
        // Upload files here
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
        Snackbar.Add("TODO: Upload your files!", Severity.Normal);

        MudDialog.Close(DialogResult.Ok(true));
        
    }

    private void SetDragClass() => DragClass = $"{DefaultDragClass} mud-border-primary";

    private void ClearDragClass() => DragClass = DefaultDragClass;

    /// <summary>
    /// Cierra el diálogo sin hacer ninguna acción.
    /// </summary>
    void Cancel() => MudDialog.Cancel();
}
