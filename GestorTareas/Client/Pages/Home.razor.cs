using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Pages;

/// <summary>
/// Página prinipal de la web
/// </summary>
public partial class Home
{
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    [Inject] protected EtiquetasHttpClient HttpEtiquetas { get; set; } = default!;
    [Inject] protected IDialogService DialogService { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;

    private TareaDTO[]? Tareas = default;
    private EtiquetaDTO[]? Etiquetas = default!;

    /// <summary>
    /// Obtiene el listado de todas las tareas y lo asigna al atributo Tareas
    /// </summary>
    private async Task CargarTareasAsync()
    {
        Tareas = await HttpTareas.ListAsync();
        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// Obtiene el listado de todas las etiquetas y lo asigna al atributo Etiquetas
    /// </summary>
    /// <returns></returns>
    private async Task CargarEtiquetasAsync()
    {
        Etiquetas = await HttpEtiquetas.ListAsync();
        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// Cada vez que se renderiza la página se ejecuta este metodo
    /// </summary>
    /// <param name="firstRender">Indica si es la primera vez que se renderiza</param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        /// Si es la primera que se renderiza se ejecuta lo siguiente
        if (firstRender)
        {
            await CargarTareasAsync();
            await CargarEtiquetasAsync();
        }
    }

    /// <summary>
    /// Modifica el estado de la tarea
    /// </summary>
    /// <param name="isCompleted">Nuevo estado de la tarea; true=completada, false=no completada</param>
    /// <param name="id">Id de la tarea</param>
    protected async Task UpdateTareasCompleted(bool isCompleted, Guid id)
    {
        // Creamos el DTO y hacemos la petición al servidor
        var idRequest = new IdRequestDTO(id);
        var successResponse = isCompleted ? await HttpTareas.CompleteAsync(idRequest) : await HttpTareas.SetPendingAsync(idRequest);

        // Si no se ha podido añadir, mostramos un mensaje de error
        if (!successResponse)
        {
            SnackbarError();
            return;
        }

        // Si se ha modificado, actualizamos la página
        await CargarTareasAsync();
    }

    /// <summary>
    /// Actualiza la página
    /// </summary>
    /// <param name="update">Booleano para saber si hay que actualizar no</param>
    protected async Task UpdatePage(bool update)
    {
        if (update)
            await CargarTareasAsync();
    }

    /// <summary>
    /// Muestra una notificación indicando que la tarea no se ha podido completar
    /// </summary>
    protected void SnackbarError() => Snackbar.Add("Error en completar la tarea", Severity.Error);

    /// <summary>
    /// Abre un popup para la creación de una nueva tarea
    /// </summary>
    private async Task CreateTarea()
    {
        // Definimos los parametros del diálogo
        var parameters = new DialogParameters
        {
            {"IsCreate",  true }
        };

        // Definimos las opciones del diálogo
        var options = new DialogOptions()
        {
            DisableBackdropClick = true
        };

        // Mostramos el diálogo y obtenemos el resultado
        var dialog = DialogService.Show<TareaDialog>("Crear Tarea", parameters, options);
        var result = await dialog.Result;

        // Si se ha creado la tarea, volvemos a cargar las tareas
        if (result.Cancelled)
            return;

        await CargarTareasAsync();
        return;
    }

}
