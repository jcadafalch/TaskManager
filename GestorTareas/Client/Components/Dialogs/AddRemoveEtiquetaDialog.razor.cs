using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Linq;
using static System.Net.WebRequestMethods;

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

    private EtiquetaDTO Etiqueta { get; set; } = default!;

    private async Task CargarEtiquetasAsync()
    {
        Etiquetas = await HttpEtiquetas.GetListEtiquetaAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (Action == "Add")
                await CargarEtiquetasAsync();

            if (Action == "Remove")
            {
                if (Tarea.Etiquetas is not null && Tarea.Etiquetas.Any())
                {
                    Etiquetas = Tarea.Etiquetas.ToArray();
                    await InvokeAsync(StateHasChanged);
                }

            }
        }
    }

    protected async Task OnValueSelected(IEnumerable<EtiquetaDTO> items)
    {
        if (items.Count() > 1 && !items.Any())
            return;

        Etiqueta = items.First();
    }

    private Func<EtiquetaDTO, string> EtiquetaToString => e => e.Name;

    private async Task Submit()
    {
        if (Action == "Add")
        {

            if (Tarea.Etiquetas.Where(e => e.Id == Etiqueta.Id).FirstOrDefault() != null)
            {
                Snackbar.Add("La etiqueta " + Etiqueta.Name + " ya esta añadida a la tarea " + Tarea.Title, Severity.Warning);
                Cancel();
                return;
            }
            else
            {
                var addEtiquetaTarea = new ManageEtiquetaTareaRequestDTO(Tarea.Id, Etiqueta.Id);
                var response = await HttpTareas.GetAddEtiquetaToTareaAsync(addEtiquetaTarea);

                if (!response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Ha habido un error en añadir la etiqueta " + Etiqueta.Name + " a la tarea " + Tarea.Title, Severity.Error);
                    return;
                }

                Snackbar.Add("Se ha añadido la etiqueta " + Etiqueta.Name + " a la tarea " + Tarea.Title, Severity.Success);

            }
        }

        if (Action == "Remove")
        {
            var removeEtiquetaTarea = new ManageEtiquetaTareaRequestDTO(Tarea.Id, Etiqueta.Id);
            var response = await HttpTareas.GetRemoveEtiquetaToTareaAsync(removeEtiquetaTarea);

            if (!response.IsSuccessStatusCode)
            {
                Snackbar.Add("Ha habido un error en retirar la etiqueta " + Etiqueta.Name + " a la tarea " + Tarea.Title, Severity.Error);
                return;
            }

            Snackbar.Add("Se ha retirado la etiqueta " + Etiqueta.Name + "de la tarea " + Tarea.Title, Severity.Success);
        }

        MudDialog.Close(DialogResult.Ok(true));
    }

    void Cancel() => MudDialog.Cancel();
}
