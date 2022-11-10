using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;

/// <summary>
/// Diálogo para añadir una etiqueta de una tarea
/// </summary>
public partial class AddEtiquetaToTareaDialog
{
    [Inject] protected EtiquetasHttpClient HttpEtiquetas { get; set; } = default!;
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public TareaDTO Tarea { get; set; } = default!;

    private EtiquetaDTO[] Etiquetas { get; set; } = default!;
    private IEnumerable<EtiquetaDTO> EtiquetasTarea { get; set; } = default!;

    /// <summary>
    /// Obtiene el listado de todas las etiquetas y lo asigna al atributo Etiquetas;
    /// también asigna al atributo EtiquetasTarea las etiquetas de la tarea
    /// </summary>
    private async Task CargarEtiquetasAsync()
    {
        Etiquetas = await HttpEtiquetas.ListAsync();
        EtiquetasTarea = Tarea.Etiquetas.ToArray();
    }

    /// <summary>
    /// Cada vez que se renderiza la página se ejecuta este método.
    /// </summary>
    /// <param name="firstRender">Indica si es la primera vez que se renderiza</param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (Tarea.Etiquetas is not null)
            {
                await CargarEtiquetasAsync();
                await InvokeAsync(StateHasChanged);
            }
                
        }
    }

    /// <summary>
    /// Añade la etiqueta a la tarea
    /// </summary>
    /// <param name="chip">MudChip de la etiqueta</param>
    private async Task AddEtiqueta(MudChip chip)
    {
        // Obtenemos la etiqueta del chip
        EtiquetaDTO etiqueta = (EtiquetaDTO)chip.Tag;

        // Si la etiqueta ya está añadida, mostramos un mensaje de error.
        if (Tarea.Etiquetas.Any(e => e.Id == etiqueta.Id))
        {
            Snackbar.Add("La etiqueta " + etiqueta.Name + " ya esta añadida a la tarea " + Tarea.Title, Severity.Warning);
            Cancel();
            return;
        }

        // Creamos el DTO i hacemos la petición al servidor
        var addEtiquetaTarea = new ManageEtiquetaTareaRequestDTO(Tarea.Id, etiqueta.Id);
        var successResponse = await HttpTareas.AddEtiquetaToTareaAsync(addEtiquetaTarea);

        // Si no se ha podido añadir, mostramos un mensaje de error
        if (!successResponse)
        { 
            Snackbar.Add($"Ha habido un error en añadir la etiqueta {etiqueta.Name} a la tarea {Tarea.Title}", Severity.Error);
            return;
        }

        // Si se ha añadido, notificamos al usuario
        Snackbar.Add("Se ha añadido la etiqueta " + etiqueta.Name + " a la tarea " + Tarea.Title, Severity.Success);


        // Cerramos el diálogo
        MudDialog.Close(DialogResult.Ok(true));
    }

    /// <summary>
    /// Cierra el diálogo sin hacer ninguna acción.
    /// </summary>
    void Cancel() => MudDialog.Cancel();
}
