using GestorTareas.Client.Http;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;

namespace GestorTareas.Client.Components.Etiqueta;

/// <summary>
/// Muestra un selecor para elegir una etiqueta
/// </summary>
public partial class SelectorEtiquetaComponent
{
    [Inject] protected EtiquetasHttpClient HttpEtiquetas { get; set; } = default!;

    [Parameter]
    public string LabelContent { get; set; } = default!;

    [Parameter]
    public EventCallback<EtiquetaDTO> OnEtiquetaSelected { get; set; }

    private EtiquetaDTO? Etiqueta;
    public EtiquetaDTO[]? Etiquetas { get; set; } = default!;

    /// <summary>
    /// Obtiene el listado de todas las tareas y lo asigna al atributo Etiquetas
    /// </summary>
    private async Task CargarEtiquetasAsync()
    {
        Etiqueta = null;
        Etiquetas = await HttpEtiquetas.ListAsync();
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
            await CargarEtiquetasAsync();
        }
    }

    /// <summary>
    /// Obtiene la etiqueta seleccionada
    /// </summary>
    /// <param name="items">IEnumerable de la etiqueta seleccionada</param>
    protected async Task OnValuesSelected(IEnumerable<EtiquetaDTO> items)
    {
        try
        {
            if (items.Count() > 1 && !items.Any())
                return;

            await OnEtiquetaSelected.InvokeAsync(items.First());
        }
        catch (Exception)
        {
        }
        
    }

    private async Task OnSelectItem(EtiquetaDTO? etiqueta)
    {
        if (etiqueta is null)
            return;

        Etiqueta = etiqueta;

        await OnEtiquetaSelected.InvokeAsync(etiqueta);
        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// Expressión lambda que devuelve el titulo de la etiqueta
    /// </summary>
    private static Func<EtiquetaDTO, string> EtiquetaToString => e => e.Name;
}
