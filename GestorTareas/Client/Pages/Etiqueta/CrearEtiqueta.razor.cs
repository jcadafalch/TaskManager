using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Client.Pages.Etiqueta;

public partial class CrearEtiqueta
{
    [Inject] protected EtiquetasHttpClient HttpEtiquetas { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }
    CreateEtiqueta Model = new CreateEtiqueta();

    public class CreateEtiqueta
    {
        [Required]
        public string Name { get; set; } = "Nombre etiqueta";
    }

    private void OnValidSubmit(EditContext context)
    {
        _ = CreateNewEtiquetaAsync();
    }

    protected async Task CreateNewEtiquetaAsync()
    {
        var etiquetadto = new CreateEtiquetaRequestDTO(Model.Name);
        var successResponse = await HttpEtiquetas.CreateAsync(etiquetadto);

        if(!successResponse)
        {
            Snackbar.Add("Ha habido un error", Severity.Error);
            return;
        }
        Snackbar.Add("La etiqueta " + Model.Name + " se ha creado correctamente", Severity.Success);
        NavigationManager.NavigateTo("/");
    }
}
