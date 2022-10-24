using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Pages.Tarea;

public partial class EliminarTarea
{
    
    
    [Inject] protected IDialogService _dialogService { get; set; } = default!;
    private TareaDTO[]? Tareas { get; set; }
    private TareaDTO Tarea { get; set; }

    protected async void GetTareaSelected(TareaDTO tarea)
    {
        Tarea = tarea;
        await InvokeAsync(StateHasChanged);
    }

 }
