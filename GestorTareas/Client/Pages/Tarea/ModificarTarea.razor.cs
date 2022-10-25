using GestorTareas.Shared;

namespace GestorTareas.Client.Pages.Tarea;

public partial class ModificarTarea
{
    private TareaDTO[]? Tareas { get; set; }
    private TareaDTO? Tarea { get; set; } = null;

    protected async void GetTareaSelected(TareaDTO tarea)
    {
        Tarea = tarea;
        await InvokeAsync(StateHasChanged);
    }

}
