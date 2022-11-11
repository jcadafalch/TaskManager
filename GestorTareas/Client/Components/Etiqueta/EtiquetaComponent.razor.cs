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
    public bool IsModify { get; set; } = default!;

    [Parameter]
    public bool IsDelete { get; set; } = default!;


    #region Modify
    /// <summary>
    /// Muestra un diálogo para modificar la etiqueta
    /// </summary>
    protected async Task ModifyEtiquetaAsync() => await ShowDialog(Etiqueta.Name, true, false, "Editar");

    #endregion

    #region Delete
    /// <summary>
    /// Muestra un diálogo para eliminar la etiqueta
    /// </summary>
    protected async Task DeleteEtiquetaAsync() => await ShowDialog("", false, true, "¡ATENCIÓN!");

    #endregion

    /// <summary>
    /// Muestra un dialogo para realizar la acción deseada
    /// </summary>
    /// <param name="Name">Nombre de la etiqueta</param>
    /// <param name="Modify">true si queremos modificar; false si no.</param>
    /// <param name="Delete">true si queremos eliminar; false si no</param>
    /// <param name="DialogTitle">Titulo del dialogo</param>
    private async Task ShowDialog(string? Name, bool Modify, bool Delete, string DialogTitle)
    {
        // Definimos los parametros del diálogo
        var parameters = new DialogParameters
        {
            {"Contenido", Name},
            {"Etiqueta", Etiqueta },
            {"IsModify", Modify},
            {"IsDelete", Delete }
        };

        // Mostramos el diálogo y obtenemos el resultado
        var dialog = DialogService.Show<EtiquetaDialog>(DialogTitle, parameters);
        var result = await dialog.Result;

        // Si se ha cancelado la acción
        if (result.Cancelled)
            return;

        if(Modify)
            NavigationManager.NavigateTo("modificar-tarea", false, true);

        if(Delete)
            NavigationManager.NavigateTo("modificar-tarea", false, true);
    }
}