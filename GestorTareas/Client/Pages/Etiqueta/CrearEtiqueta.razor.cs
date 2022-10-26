using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Client.Pages.Etiqueta;

public partial class CrearEtiqueta
{
    [Inject] protected EtiquetasHttpClient Http { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }
    CreateEtiqueta Model = new CreateEtiqueta();

    public class CreateEtiqueta
    {
        [Required]
        public string Name { get; set; }
    }

    private void OnValidSubmit(EditContext context)
    {
        _ = CreateNewEtiquetaAsync();
    }

    protected async Task CreateNewEtiquetaAsync()
    {
        var etiquetadto = new CreateEtiquetaRequestDTO(Model.Name);
        var response = await Http.GetCreateEtiquetaAsync(etiquetadto);

        if(!response.IsSuccessStatusCode)
        {
            Snackbar.Add("Ha habido un error", Severity.Error);
            return;
        }

        //await InvokeAsync(StateHasChanged);
        NavigationManager.NavigateTo("/");
    }
}
