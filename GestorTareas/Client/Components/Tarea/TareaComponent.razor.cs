using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Tarea;

/// <summary>
/// Muestra una tarea
/// </summary>
public partial class TareaComponent
{
    [Inject] IDialogService DialogService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public TareaDTO Tarea { get; set; } = default!;

    [Parameter]
    public bool TareaStatus { get; set; } = default!;

    [Parameter]
    public bool IsModify { get; set; } = default!;

    [Parameter]
    public bool IsDelete { get; set; } = default!;

    [Parameter]
    public EventCallback<bool> OnStatusChanged { get; set; } = default!;

    private async Task CheckBoxChanged(bool e)
    {
        TareaStatus = e;
        await OnStatusChanged.InvokeAsync(TareaStatus);
    }

    /// <summary>
    /// Muestra un diálogo para modificar la tarea.
    /// </summary>
    private async void ModifyTareaAsync() => await ShowDialog(Tarea.Content, true, false, "Editar");

    /// <summary>
    /// Muestra un diálogo para editar la tarea.
    /// </summary>
    private async void DeleteTareaAsync() => await ShowDialog("", false, true, "¡AVISO!");

    /// <summary>
    /// Muestra un dialogo para realizar la acción deseada
    /// </summary>
    /// <param name="Contenido">Contenido de la tarea</param>
    /// <param name="Modify">true si queremos modificar; false si no.</param>
    /// <param name="Delete">true si queremos eliminar; false si no</param>
    /// <param name="DialogTitle">Titulo del dialogo</param>
    private async Task ShowDialog(String Contenido, bool Modify, bool Delete, String DialogTitle)
    {
        // Definimos los parametros del diálogo
        var parameters = new DialogParameters
        {
            {"Contenido", Contenido },
            {"Tarea", Tarea },
            {"IsDelete", Delete},
            {"IsModify", Modify }
        };

        // Mostramos el dialogo y obtenemos el resultado
        var dialog = DialogService.Show<TareaDialog>(DialogTitle, parameters);
        var result = await dialog.Result;

        // Si se ha realizado la acción
        if (!result.Cancelled)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
