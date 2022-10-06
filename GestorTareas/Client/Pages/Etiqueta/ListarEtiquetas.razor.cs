using GestorTareas.Shared;
using System.Net.Http.Json;

namespace GestorTareas.Client.Pages.Etiqueta
{
    public partial class ListarEtiquetas
    {
        HttpClient Http;

        private EtiquetaDTO[]? etiquetas;

        private async Task CargarEtiquetasAsync()
        {
            etiquetas = await Http.GetFromJsonAsync<EtiquetaDTO[]>("api/gestortareas");
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await CargarEtiquetasAsync();
            }
        }
    }
}
