using GestorTareas.Shared;

namespace GestorTareas.Client.Pages.Etiqueta;

public partial class EliminarEtiqueta
{
    private EtiquetaDTO[]? Etiquetas { get; set; }
    private EtiquetaDTO Etiqueta { get; set; }

    protected async void GetEtiquetaSelected(EtiquetaDTO etiqueta)
    {
        Etiqueta = etiqueta;
        await InvokeAsync(StateHasChanged);
    }
}
