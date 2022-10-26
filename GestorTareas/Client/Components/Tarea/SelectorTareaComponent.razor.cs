using GestorTareas.Client.Models;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;

namespace GestorTareas.Client.Components.Tarea;

public partial class SelectorTareaComponent
{
    [Inject] protected TareasHttpClient Http { get; set; } = default!;

    [Parameter]
    public TareaDTO[]? Tareas { get; set; } = default!;

    [Parameter]
    public string LabelContent { get; set; } = default!;

    [Parameter]
    public EventCallback<TareaDTO> OnTareaSelected { get; set; }

    private async Task CargarTareasAsync()
    {
        Tareas = await Http.GetListTareaAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        //* Si es comenta el if i es deixa només la funcio, no para de cridar a la api --> No es efficient
        if (firstRender)
        {
            await CargarTareasAsync();
        }
    }

    protected async Task OnValuesSelected(IEnumerable<TareaDTO> items)
    {
        if (items.Count() > 1 && !items.Any())
            return;

        await OnTareaSelected.InvokeAsync(items.First());
    }
    private Func<TareaDTO, string> TareaToString => t => t.Title;

}
