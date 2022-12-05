using GestorTareas.Client.Http;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Client.Pages.Etiqueta;

/// <summary>
/// Página crear etiqueta
/// </summary>
public partial class CrearEtiqueta
{
    [Inject] protected EtiquetasHttpClient HttpEtiquetas { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;

    /// <summary>
    /// Classe con los atributos del modelo del formulario
    /// </summary>
    CreateEtiqueta Model = new CreateEtiqueta();

    public class CreateEtiqueta
    {
        [Required]
        public string Name { get; set; } = "Nombre etiqueta";
    }

    /// <summary>
    /// Cuando formulario se envia de forma satisfactoria, crea la nueva tarea
    /// </summary>
    /// <param name="context">Contexto del formulario</param>
    private void OnValidSubmit(EditContext context)
    {
        _ = CreateNewEtiquetaAsync();
    }

    /// <summary>
    /// Gestiona la creación de una nueva etiqueta
    /// </summary>
    /// <returns>Muestra una notificación al usuario si el proceso ha sido satisfactorio o no</returns>
    protected async Task CreateNewEtiquetaAsync()
    {
        // Creamos el DTO y hacemos la petición al servidor
        var etiquetadto = new CreateEtiquetaRequestDTO(Model.Name);
        var successResponse = await HttpEtiquetas.CreateAsync(etiquetadto);

        // Si no se ha podido añadir, mostramos un mensaje de error
        if (!successResponse)
        {
            Snackbar.Add("Ha habido un error", Severity.Error);
            return;
        }

        // Si se ha añadido, notificamos al usuario
        Snackbar.Add($"La etiqueta {Model.Name} se ha creado correctamente", Severity.Success);
        NavigationManager.NavigateTo("listar-etiquetas");
    }
}
