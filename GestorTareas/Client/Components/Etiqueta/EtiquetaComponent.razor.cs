using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Etiqueta;

/// <summary>
/// Muestra una etiqueta
/// </summary>
public partial class EtiquetaComponent
{
    [Inject] IDialogService DialogService { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public EtiquetaDTO Etiqueta { get; set; } = default!;

    [Parameter]
    public bool Modify { get; set; } = default!;

    [Parameter]
    public bool Delete { get; set; } = default!;

    #region Modify
    /// <summary>
    /// Muestra un diálogo para modificar la etiqueta
    /// </summary>
    protected async Task ModifyEtiquetaAsync()
    {
        // Definimos los parametros del diálogo
        var parameters = new DialogParameters
        {
            {"Contenido", Etiqueta.Name },
            {"Etiqueta", Etiqueta },
            {"Modify", true}
        };

        // Mostramos el diálogo y obtenemos el resultado
        var dialog = DialogService.Show<EtiquetaDialog>("Editar", parameters);
        var result = await dialog.Result;

        // Si se ha modificado la etiqueta, notificamos al componente que ha cambiado y navegamos a la pagina home.
        if (!result.Cancelled)
        {
            await InvokeAsync(StateHasChanged);
            NavigationManager.NavigateTo("/");
        }
    }
    #endregion

    #region Delete
    /// <summary>
    /// Muestra un diálogo para eliminar la etiqueta
    /// </summary>
    protected async Task DeleteEtiquetaAsync()
    {
        // Definimos los parametros del dialogo
        var parameters = new DialogParameters
        {
            {"Contenido", "" },
            {"Etiqueta", Etiqueta},
            {"Delete", true }
        };

        // Mostramos el diálogo y obtenemos el resultado
        var dialog = DialogService.Show<EtiquetaDialog>("¡ATENCIÓN!", parameters);
        var result = await dialog.Result;

        // Si se ha eliminado la etiqueta, notificamos al componente que ha cambiado y navegamos a la pagina home
        if (!result.Cancelled)
        {
            await InvokeAsync(StateHasChanged);
            NavigationManager.NavigateTo("/");
        }
    }

    #endregion
}