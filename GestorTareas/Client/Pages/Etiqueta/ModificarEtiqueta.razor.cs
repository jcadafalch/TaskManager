using GestorTareas.Shared;

namespace GestorTareas.Client.Pages.Etiqueta;

/// <summary>
/// Página modificar etiqueta
/// </summary>
public partial class ModificarEtiqueta
{
    private EtiquetaDTO? Etiqueta { get; set; } = null;

    /// <summary>
    /// Obtiene la etiqueta seleccionada en el selector y la asigna al atributo Etiqueta
    /// </summary>
    /// <param name="etiqueta">DTO de etiqueta</param>
    protected async void GetEtiquetaSelected(EtiquetaDTO etiqueta)
    {
        Etiqueta = etiqueta;
        await InvokeAsync(StateHasChanged);
    }
}
