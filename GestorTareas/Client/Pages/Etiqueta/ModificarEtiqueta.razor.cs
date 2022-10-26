using GestorTareas.Shared;

namespace GestorTareas.Client.Pages.Etiqueta;

public partial class ModificarEtiqueta
{
    private EtiquetaDTO[]? Etiquetas { get; set; }
    private EtiquetaDTO? Etiqueta { get; set; } = null;

    protected async void GetEtiquetaSelected(EtiquetaDTO etiqueta)
    {
        Etiqueta = etiqueta;
        await InvokeAsync(StateHasChanged);
    }
}
