using GestorTareas.Shared;

namespace GestorTareas.Client.Pages.Tarea;

/// <summary>
/// Pagina eliminar tarea
/// </summary>
public partial class EliminarTarea
{
    private TareaDTO Tarea { get; set; }

    /// <summary>
    /// Obtiene la tarea seleccionada en el selector i la asigna al atributo Tarea
    /// </summary>
    /// <param name="tarea">DTO de tarea</param>
    protected async void GetTareaSelected(TareaDTO tarea)
    {
        Tarea = tarea;
        await InvokeAsync(StateHasChanged);
    }

}
