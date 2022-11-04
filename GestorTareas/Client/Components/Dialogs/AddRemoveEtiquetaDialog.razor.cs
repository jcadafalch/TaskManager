using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Dialogs;

public partial class AddRemoveEtiquetaDialog
{
    [Inject] protected EtiquetasHttpClient HttpEtiquetas { get; set; } = default!;
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public string LabelContent { get; set; } = default!;

    [Parameter]
    public string Action { get; set; } = default!;

    [Parameter]
    public TareaDTO Tarea { get; set; }

    private EtiquetaDTO[] Etiquetas { get; set; } = default!;
    private EtiquetaDTO[] TareaEtiquetas { get; set; } = default;

    private async Task CargarEtiquetasAsync()
    {
        Etiquetas = await HttpEtiquetas.GetListEtiquetaAsync();
        TareaEtiquetas = Tarea.Etiquetas.ToArray();
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (Tarea.Etiquetas is not null)
                await CargarEtiquetasAsync();
        }
    }

    private async Task Submit(MudChip chip)
    {
        EtiquetaDTO etiqueta = (EtiquetaDTO)chip.Tag;
        if (Tarea.Etiquetas.Where(e => e.Id == etiqueta.Id).FirstOrDefault() != null)
        {
            Snackbar.Add("La etiqueta " + etiqueta.Name + " ya esta añadida a la tarea " + Tarea.Title, Severity.Warning);
            Cancel();
            return;
        }
        else
        {
            var addEtiquetaTarea = new ManageEtiquetaTareaRequestDTO(Tarea.Id, etiqueta.Id);
            var response = await HttpTareas.GetAddEtiquetaToTareaAsync(addEtiquetaTarea);

            if (!response.IsSuccessStatusCode)
            {
                Snackbar.Add("Ha habido un error en añadir la etiqueta " + etiqueta.Name + " a la tarea " + Tarea.Title, Severity.Error);
                return;
            }

            Snackbar.Add("Se ha añadido la etiqueta " + etiqueta.Name + " a la tarea " + Tarea.Title, Severity.Success);

        }

        MudDialog.Close(DialogResult.Ok(true));
    }

    void Cancel() => MudDialog.Cancel();
}
