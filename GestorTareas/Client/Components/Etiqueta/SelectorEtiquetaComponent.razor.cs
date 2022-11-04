using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;

namespace GestorTareas.Client.Components.Etiqueta;

public partial class SelectorEtiquetaComponent
{
    [Inject] protected EtiquetasHttpClient HttpEtiquetas { get; set; } = default!;

    [Parameter]
    public EtiquetaDTO[]? Etiquetas { get; set; } = default!;

    [Parameter]
    public string LabelContent { get; set; } = default!;

    [Parameter]
    public EventCallback<EtiquetaDTO> OnEtiquetaSelected { get; set; }


    private async Task CargarEtiquetasAsync()
    {
        Etiquetas = await HttpEtiquetas.ListAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await CargarEtiquetasAsync();
        }
    }

    protected async Task OnValuesSelected(IEnumerable<EtiquetaDTO> items)
    {
        if (items.Count() > 1 && !items.Any())
            return;

        await OnEtiquetaSelected.InvokeAsync(items.First());
    }

    private Func<EtiquetaDTO, string> EtiquetaToString => e => e.Name;
}
