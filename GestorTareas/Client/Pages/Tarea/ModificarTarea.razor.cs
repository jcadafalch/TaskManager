using GestorTareas.Shared;

namespace GestorTareas.Client.Pages.Tarea;

/// <summary>
/// Pagina modificar tarea
/// </summary>
public partial class ModificarTarea
{
    private TareaDTO? Tarea { get; set; } = null;

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
