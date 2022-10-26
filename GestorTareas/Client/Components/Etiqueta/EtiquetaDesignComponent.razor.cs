using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;

namespace GestorTareas.Client.Components.Etiqueta;

public partial class EtiquetaDesignComponent
{
    [Parameter]
    public EtiquetaDTO Etiqueta { get; set; } = default!;
}
