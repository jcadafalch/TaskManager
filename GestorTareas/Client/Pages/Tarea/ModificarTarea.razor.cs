using GestorTareas.Client.Http;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;

namespace GestorTareas.Client.Pages.Tarea;

/// <summary>
/// Página modificar tarea
/// </summary>
public partial class ModificarTarea
{
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    private TareaDTO[]? Tareas = default;

    /// <summary>
    /// Obtiene el listado de todas las tareas y lo asigna al atributo tareas
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

    private async Task UpdatePage()
    {
        await CargarTareasAsync();
    }
}
