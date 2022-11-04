using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Client.Pages.Tarea;

public partial class CrearTarea
{
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }
    CreateTarea model = new CreateTarea();

    public class CreateTarea
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }

    private void OnValidSubmit(EditContext context)
    {
        _ = CreateNewTareaAsync();
    }

    protected async Task CreateNewTareaAsync()
    {
        var tareadto = new CreateTareaRequestDTO(model.Title, model.Content);
        var successResponse = await HttpTareas.CreateAsync(tareadto);

        if (!successResponse)
        {
            Snackbar.Add("Ha habido un error", Severity.Error);
            return;
        }

        Snackbar.Add("La tarea " + model.Title + " se ha creado correctamente", Severity.Success);
        await InvokeAsync(StateHasChanged);
        NavigationManager.NavigateTo("/");

    }
}
