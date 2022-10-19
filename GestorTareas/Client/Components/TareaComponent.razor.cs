using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components;

public partial class TareaComponent
{
    [Inject] IDialogService Dialog { get; set; } = default!;

    [Parameter]
    public TareaDTO Tarea { get; set; } = default!;

    [Parameter]
    public bool TareaStatus { get; set; } = default;


    [Parameter]
    public EventCallback<bool> OnStatusChanged { get; set; }


    protected async Task CheckBoxChanged(bool e)
    {
        Console.WriteLine(TareaStatus);
        TareaStatus = e;
        Console.WriteLine(TareaStatus);
        await InvokeAsync(StateHasChanged);
        await OnStatusChanged.InvokeAsync(TareaStatus);
    }

    protected void ModifyTarea()
    {
        var parameters = new DialogParameters
        {
            { "Contenido", Tarea.Content },
            { "Tarea", Tarea }
        };

        var options = new DialogOptions()
        {
            DisableBackdropClick = true
        };

        Dialog.Show<ModificarTareaDialog>("Editar", parameters);
    }
}

