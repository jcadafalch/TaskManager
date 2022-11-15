using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;

namespace GestorTareas.Client.Pages.Etiqueta;

/// <summary>
/// Página modificar etiqueta
/// </summary>
public partial class ModificarEtiqueta
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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await CargarEtiquetasAsync();
    }

    private async Task UpdatePage()
    {
        await CargarEtiquetasAsync();
    }
}
