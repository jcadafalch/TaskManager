using GestorTareas.Shared;

namespace GestorTareas.Client.Pages.Etiqueta;

/// <summary>
/// Página eliminar etiqueta
/// </summary>
public partial class EliminarEtiqueta
{
    private EtiquetaDTO Etiqueta { get; set; }

    /// <summary>
    /// Obtiene la etiqueta seleccionada del selecor y la asigna al atributo Etiqueta
    /// </summary>
    /// <param name="etiqueta">DTO de etiqueta</param>
    protected async void GetEtiquetaSelected(EtiquetaDTO etiqueta)
    {
        Etiqueta = etiqueta;
        await InvokeAsync(StateHasChanged);
    }
}
