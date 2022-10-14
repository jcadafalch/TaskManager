using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace GestorTareas.Client.Pages.Tarea;

public partial class CrearTarea
{
    [Inject] protected HttpClient Http { get; set; } = default!;
    CreateTarea model = new CreateTarea();
    //bool success;

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
        await Http.PostAsJsonAsync("/api/gestortareas/createtarea", tareadto);
        StateHasChanged();

    }
}
