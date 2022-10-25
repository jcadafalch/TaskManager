using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;

namespace GestorTareas.Client.Components;

public partial class EtiquetaComponent
{
    [Parameter]
    public EtiquetaDTO Etiqueta { get; set; } = default!;
}
