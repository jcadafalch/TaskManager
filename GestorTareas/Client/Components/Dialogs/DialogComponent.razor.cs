using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;

/// <summary>
/// Diálogo genérico para realizar alguna acción.
/// </summary>
public partial class DialogComponent
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;

    [Parameter] public string ContentText { get; set; } = default!;

    [Parameter] public string ButtonText { get; set; } = default!;

    [Parameter] public Color Color { get; set; } = default!;

    /// <summary>
    /// Cierra el diálogo indicando que se acepta la acción
    /// </summary>
    void Submit() => MudDialog.Close(DialogResult.Ok(true));

    /// <summary>
    /// Cierra el diálogo sin hacer ninguna acción.
    /// </summary>
    void Cancel() => MudDialog.Cancel();
}
