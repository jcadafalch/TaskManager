using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using static MudBlazor.Colors;

namespace GestorTareas.Client.Components.Tarea;

/// <summary>
/// Muestra una tarea con todas las acciones que puede realizar
/// </summary>
public partial class CompleteTareaComponent
{
    [Inject] IDialogService DialogService { get; set; } = default!;
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public TareaDTO Tarea { get; set; } = default!;

    [Parameter]
    public EtiquetaDTO[] Etiquetas { get; set; } = default!;

    [Parameter]
    public bool TareaStatus { get; set; } = default;

    [Parameter]
    public EventCallback<bool> OnStatusChanged { get; set; }

    [Parameter]
    public EventCallback<bool> OnTareaChanged { get; set; }

    [Parameter]
    public bool IsDarkMode { get; set; } = default!;

    #region Estado de la tarea

    /// <summary>
    /// Informa al la classe que contiene el componente que el estado de la tarea ha cambiado y con que valor
    /// </summary>
    protected async Task OnChangeStatus()
    {
        TareaStatus = !TareaStatus;
        await OnStatusChanged.InvokeAsync(TareaStatus);
    }
    #endregion

    #region Modificar
    /// <summary>
    /// Muestra un dialogo para modificar la tarea
    /// </summary>
    protected async Task ModifyTarea() => await ShowDialog(Tarea.Content, true, false, "Editar");
    #endregion

    #region Eliminar
    /// <summary>
    /// Muestra un dialogo para eliminar la tarea
    /// </summary>
    private async Task DeleteTareaAsync() => await ShowDialog("", false, true, "¡AVISO!");
    #endregion

    #region Etiquetas

    /// <summary>
    /// Muestra un dialogo para añadir una etiqueta a la tarea
    /// </summary>
    private async Task AddEtiquetaToTarea()
    {
        // Definimos los parametros del dialogo
        var parameters = new DialogParameters
        {
            {"Tarea", Tarea }
        };

        // Definimos las opciones del dialogo
        var options = new DialogOptions
        {
            CloseButton = true,
            DisableBackdropClick = false,

        };

        // Mostramos el dialogo y obtenemos el resultado
        var dialog = DialogService.Show<AddEtiquetaToTareaDialog>("Añadir etiqueta a tarea", parameters, options);
        var result = await dialog.Result;

        // Si se ha añadido una etiqueta a la tarea, actualizamos la pagina
        if (!result.Cancelled)
        {
            await UpdatePage();
            return;
        }
    }

    /// <summary>
    /// Muestra un dialogo para retirar la etiqueta de la tarea
    /// </summary>
    /// <param name="chip">MudChip de la etiqueta a retirar</param>
    private async Task RemoveEtiquetaToTarea(MudChip chip)
    {
        // Obtenemos la etiqueta del chip
        EtiquetaDTO etiqueta = (EtiquetaDTO)chip.Tag;

        // Definimos los parametros del dialogo
        var parameters = new DialogParameters
        {
            {"Tarea", Tarea },
            {"Etiqueta", etiqueta },
        };

        // Definimos las opciones del dialogo
        var options = new DialogOptions
        {
            CloseButton = true,
            DisableBackdropClick = false,

        };

        // Mostramos el dialogo y obtenemos el resultado
        var dialog = DialogService.Show<RemoveEtiquetaToTareaDialog>("¡ATENCIÓN!", parameters, options);
        var result = await dialog.Result;

        // Si se ha retirado la etiqueta, actualizamos la pagina
        if (!result.Cancelled)
        {
            await UpdatePage();
            return;

        }
    }

    #endregion

    #region ShowDialog
    /// <summary>
    /// Muestra un dialogo para realizar la acción deseada
    /// </summary>
    /// <param name="Contenido">Contenido de la tarea</param>
    /// <param name="Modify">true si queremos modificar; false si no.</param>
    /// <param name="Delete">true si queremos eliminar; false si no</param>
    /// <param name="DialogTitle">Titulo del dialogo</param>
    private async Task ShowDialog(string? Contenido, bool Modify, bool Delete, string DialogTitle)
    {
        // Definimos los parametros del diálogo
        var parameters = new DialogParameters
        {
            { "Contenido", Contenido},
            { "Tarea", Tarea },
            { "IsModify", Modify },
            {"IsDelete", Delete}
        };

        // Mostramos el dialogo y obtenemos el resultado
        var dialog = DialogService.Show<TareaDialog>(DialogTitle, parameters);
        var result = await dialog.Result;

        if (result.Cancelled)
            return;

        await UpdatePage();
        return;
    }
    #endregion

    /// <summary>
    /// Actualiza la pagina actual
    /// </summary>
    private async Task UpdatePage()
    {
        await InvokeAsync(StateHasChanged);
        await OnTareaChanged.InvokeAsync(true);
    }
}

