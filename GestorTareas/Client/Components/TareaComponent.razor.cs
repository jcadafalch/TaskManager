using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;

namespace GestorTareas.Client.Components;

public partial class TareaComponent
{
    [Parameter]
    public TareaDTO Tarea { get; set; } = default;

    [Parameter]
    public EventCallback<bool> OnStatusChanged { get; set; }


    public string? backgorund { get; set; }

    private string StringStatus(ChangeEventArgs e)
    {
        if (Tarea.IsCompleted)
        {
            backgorund = "@($\"background-color:{Colors.Red.Lighten4};";
            _ = CheckBoxChanged(e);
            return "Descompletar Tarea";
        }
        else
        {
            backgorund = "@($\"background-color:{Colors.Green.Lighten4};";
            _ = CheckBoxChanged(e);
            return "Completar Tarea";
        }
    }

    
    protected async Task CheckBoxChanged(ChangeEventArgs e)
    {
        bool isCompleted = !Tarea.IsCompleted;
        await OnStatusChanged.InvokeAsync(isCompleted);
    }
}

