using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;

namespace GestorTareas.Client.Pages.Etiqueta;

/// <summary>
/// Página listar etiquetas
/// </summary>
public partial class ListarEtiquetas
{
    [Inject] protected EtiquetasHttpClient HttpEtiquetas { get; set; } = default!;

    private EtiquetaDTO[]? Etiquetas;

    /// <summary>
    /// Obtiene el listado de todas las etiquetas y lo asigna al atributo etiquetas
    /// </summary>
    private async Task CargarEtiquetasAsync()
    {
        Etiquetas = await HttpEtiquetas.ListAsync();
        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// Cada vez que se renderiza la página se ejecuta este metodo
    /// </summary>
    /// <param name="firstRender">Indica si es la primera vez que se renderiza</param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await CargarEtiquetasAsync();
        }
    }
}
