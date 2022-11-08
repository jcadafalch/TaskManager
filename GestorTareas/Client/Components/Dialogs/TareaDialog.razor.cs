using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;
/// <summary>
/// Muestra un diálogo con las acciones de tareas
/// </summary>
public partial class TareaDialog
{
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; }
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public TareaDTO Tarea { get; set; }

    [Parameter] 
    public string Contenido { get; set; }

    [Parameter]
    public bool Create { get; set; } = default!;

    [Parameter]
    public bool Modify { get; set; } = default!;

    [Parameter]
    public bool Delete { get; set; } = default!; 

    public string Titulo { get; set; }

    /// <summary>
    /// Modificamos la tarea
    /// </summary>
    private async Task ModifyAsync()
    {
        // Creamos el DTO y hacemos la petición al servidor
        var updatetareadto = new UpdateTareaRequestDTO(Tarea.Id, Contenido);
        var successResponse = await HttpTareas.UpdateAsync(updatetareadto);

        // Si no se ha podido añadir, mostramos un mensaje de error
        if (!successResponse)
        {
            Snackbar.Add("Ha habido un error en modificar la tarea", Severity.Error);
            return;
        }

        // Si se ha añadido, notificamos al usuario
        Snackbar.Add("La Tarea " + Tarea.Title + " se ha modificado correctamente", Severity.Success);

        // Cerramos el diálogo
        MudDialog.Close(DialogResult.Ok(true));
    }

    /// <summary>
    /// Eliminamos la tarea
    /// </summary>
    private async Task DeleteAsync()
    {
        // Creamos el DTO y hacemos la petición al servidor
        var deletetareadto = new IdRequestDTO(Tarea.Id);
        var successResponse = await HttpTareas.DeleteAsync(deletetareadto);

        // Si no se ha podido añadir, mostramos un mensaje de error
        if (!successResponse)
        {
            Snackbar.Add("Ha habido un error en eliminar la tarea", Severity.Error);
            return;
        }

        // Si se ha añadido, notificamos al usuario
        Snackbar.Add("La tarea " + Tarea.Title + " se ha eliminado correctamente", Severity.Success);

        // Cerramos el diálogo
        MudDialog.Close(DialogResult.Ok(true));
    }

    /// <summary>
    /// Creamos la tarea
    /// </summary>
    private async Task CreateAsync()
    {
        // Creamos el DTO y hacemos la petición al servidor
        var tareadto = new CreateTareaRequestDTO(Titulo, Contenido);
        var successResponse = await HttpTareas.CreateAsync(tareadto);

        // Si no se ha podido añadir, mostramos un mensaje de error
        if (!successResponse)
        {
            Snackbar.Add("Ha habido un error en crear la tarea", Severity.Error);
            return;
        }

        // Si se ha añadido, notificamos al usuario
        Snackbar.Add("La tarea " + Titulo + " se ha creado correctamente", Severity.Success);

        // Cerramos el diálogo
        MudDialog.Close(DialogResult.Ok(true));
    }

    /// <summary>
    /// Cierra el diálogo sin hacer ninguna acción.
    /// </summary>
    void Cancel() => MudDialog.Cancel();
}
