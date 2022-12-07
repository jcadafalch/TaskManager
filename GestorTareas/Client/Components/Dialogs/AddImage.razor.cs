using GestorTareas.Client.Http;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;

public partial class AddImage
{
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public TareaDTO Tarea { get; set; } = default!;

    private IList<IBrowserFile> Files = new List<IBrowserFile>();
    private int MaxAllowdFiles = int.MaxValue; //Canivar més tard per valors més exactes
    private long MaxSizeFiles = long.MaxValue; //Canivar més tard per valors més exactes

    private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full z-10";
    private string DragClass = DefaultDragClass;

    private async void Upload()
    {
        List<string>? notUploadFiles = new();
        foreach (var file in Files)
        {

            /* var destPath = Path.GetTempPath();
             Console.WriteLine(destPath);
             Console.WriteLine(File.Exists(destPath + file.Name));
             Console.WriteLine(Path.Combine(file.Name));*/

            using Stream s = file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 1024);
            using MemoryStream ms = new MemoryStream();
            await s.CopyToAsync(ms);
            byte[] fileBytes = ms.ToArray();

            Console.WriteLine(fileBytes.ToString());
            Console.WriteLine(fileBytes.Length);
            Console.WriteLine(new FileInfo(file.Name).Extension);
            string extn = new FileInfo(file.Name).Extension;

            var addArchivoTarea = new AddArchivoTareaRequestDTO(Tarea.Id, file.Name, fileBytes, extn);
            var successResponse = await HttpTareas.AddArchivoToTareaAsync(addArchivoTarea);

            if (!successResponse)
            {
                notUploadFiles.Add(file.Name);
            }
        }

        if (notUploadFiles.Count > 0)
        {
            Snackbar.Configuration.SnackbarVariant = Variant.Filled;
            Snackbar.Add("Los siguientes ficheros no se han podido añadir:", Severity.Info);

            Snackbar.Configuration.SnackbarVariant = Variant.Outlined;
            foreach (var file in notUploadFiles)
            {
                Snackbar.Add(file, Severity.Error);
            }

            //Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            //Snackbar.Add("TODO: Upload your files!", Severity.Normal);
            MudDialog.Close(DialogResult.Ok(true));
        }

        Snackbar.Add("Se han añadido todos archivos correctamente", Severity.Success);
        MudDialog.Close(DialogResult.Ok(true));
    }

    /// <summary>
    /// Retira un fichero de la lista de ficheros a subir
    /// </summary>
    /// <param name="file">Fichero a retirar</param>
    private void RemoveFile(IBrowserFile file) => Files.Remove(file);

    private async Task OnInputFileChanged(InputFileChangeEventArgs e)
    {
        ClearDragClass();
        foreach(var file in e.GetMultipleFiles())
        {
            Files.Add(file);
        }

        //using var content = new MultipartFormDataContent();

        /*foreach (var file in e.GetMultipleFiles(MaxAllowdFiles))
        {
            using var f = file.OpenReadStream();
            using var fileContent = new StreamContent(f);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            Files.Add(file);
        }*/
    }

    private void SetDragClass()
    {
        DragClass = $"{DefaultDragClass} mud-border-primary";
    }

    private void ClearDragClass()
    {
        DragClass = DefaultDragClass;
    }

    /// <summary>
    /// Cierra el diálogo sin hacer ninguna acción.
    /// </summary>
    void Cancel() => MudDialog.Cancel();
}
