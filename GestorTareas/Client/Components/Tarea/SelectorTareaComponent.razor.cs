using GestorTareas.Client.Http;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;

namespace GestorTareas.Client.Components.Tarea;

/// <summary>
/// Muestra un selecor para elegir una tarea
/// </summary>
public partial class SelectorTareaComponent
{
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;

    [Parameter]
    public string LabelContent { get; set; } = default!;

    [Parameter]
    public EventCallback<TareaDTO> OnTareaSelected { get; set; }

    public TareaDTO[]? Tareas { get; set; } = default!;

    /// <summary>
    /// Obtiene el listado de todas las tareas y lo asigna al atributo Tareas
    /// </summary>
    private async Task CargarTareasAsync()
    {
        Tareas = await HttpTareas.ListAsync();
        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// Cada vez que se renderiza la pagina se ejecuta este metodo
    /// </summary>
    /// <param name="firstRender">Indica si es la primera vez que se renderiza</param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await CargarTareasAsync();
        }
    }

    /// <summary>
    /// Obtiene la tarea seleccionada
    /// </summary>
    /// <param name="items">IEnumerable de la tarea seleccionada</param>
    protected async Task OnValuesSelected(IEnumerable<TareaDTO> items)
    {
        if (items.Count() > 1 && !items.Any())
            return;

        await OnTareaSelected.InvokeAsync(items.First());
    }
    
    /// <summary>
    /// Expressión lambda que devuelve el titulo de la tarea
    /// </summary>
    private static Func<TareaDTO, string> TareaToString => t => t.Title;

}
