using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Client.Pages.Tarea;

/// <summary>
/// Psgina crear tarea
/// </summary>
public partial class CrearTarea
{
    [Inject] protected TareasHttpClient HttpTareas { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;

    CreateTarea Model = new();

    /// <summary>
    /// Classe con los atributos del modelo del formulario
    /// </summary>
    public class CreateTarea
    {
        [Required]
        public string Title { get; set; } = default!;

        [Required]
        public string Content { get; set; } = default!;
    }

    /// <summary>
    /// Cuando formulario se envia de forma satisfactoria, crea la nueva tarea
    /// </summary>
    /// <param name="context">Contexto del formulario</param>
    private void OnValidSubmit(EditContext context)
    {
        _ = CreateNewTareaAsync();
    }

    /// <summary>
    /// Gestiona la creación de una nueva tarea
    /// </summary>
    /// <returns>Muestra una notificación al usuario si el proceso ha sido satisfactorio o no</returns>
    protected async Task CreateNewTareaAsync()
    {
        // Creamos el DTO y hacemos la petición al servidor
        var tareadto = new CreateTareaRequestDTO(Model.Title, Model.Content);
        var successResponse = await HttpTareas.CreateAsync(tareadto);

        // Si no se ha podido añadir, mostramos un mensaje de error
        if (!successResponse)
        {
            Snackbar.Add("Ha habido un error", Severity.Error);
            return;
        }

        // Si se ha añadido, notificamos al usuario i navegamos a la pagina home
        Snackbar.Add($"La tarea {Model.Title} se ha creado correctamente", Severity.Success);
        NavigationManager.NavigateTo("/");

    }
}
