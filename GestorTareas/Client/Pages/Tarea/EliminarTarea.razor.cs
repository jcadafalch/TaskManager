using GestorTareas.Shared;

namespace GestorTareas.Client.Pages.Tarea;

public partial class EliminarTarea
{
    private TareaDTO[]? Tareas { get; set; }
    private TareaDTO Tarea { get; set; }

    protected async void GetTareaSelected(TareaDTO tarea)
    {
        Tarea = tarea;
        await InvokeAsync(StateHasChanged);
    }

}
