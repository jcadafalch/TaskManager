using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;

namespace GestorTareas.Client.Components;

public partial class TareaComponent
{
    [Parameter]
    public TareaDTO Tarea { get; set; } = default;

    public string backgorund { get; set; }

    private string StringStatus()
    {
        if (Tarea.IsCompleted)
        {
            return "Descompletar Tarea";
            backgorund = "@($\"background-color:{Colors.Red.Lighten4};";
            CheckBoxChanged();
        }
        else
        {
            return "Completar Tarea";
            backgorund = "@($\"background-color:{Colors.Green.Lighten4};";
            CheckBoxChanged();
        }
    }

    //[Parameter]
    public EventCallback<bool> OnStatusChanged { get; set; }

    protected async Task CheckBoxChanged()
    {
        bool isCompleted = !Tarea.IsCompleted;
        await OnStatusChanged.InvokeAsync(isCompleted);
    }
}

