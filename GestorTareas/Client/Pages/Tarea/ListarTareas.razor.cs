using GestorTareas.Shared;
using System.Net.Http.Json;

namespace GestorTareas.Client.Pages.Tarea;

public partial class ListarTareas
{
    HttpClient Http;
    private TareaDTO[]? tareas = default;
    public bool? value { get; set; } = false;
    private async Task CargarTareasAsync()
    {
        tareas = await Http.GetFromJsonAsync<TareaDTO[]>("api/gestortareas/listtarea");
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await CargarTareasAsync();
        }
    }

    protected async Task UpdateTareasCompleted(bool IsCompleted, Guid id)
    {
        if (IsCompleted)
        {
            var complete = await Http.PostAsJsonAsync("api/gestortareas/completetarea", id);
        }
        else
        {
            var pending = await Http.PostAsJsonAsync("apu/gestortareas/setpendingtarea", id);
        }
    }
}
