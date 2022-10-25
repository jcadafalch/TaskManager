﻿using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace GestorTareas.Client.Pages.Etiqueta
{
    public partial class ListarEtiquetas
    {
        [Inject] protected HttpClient Http { get; set; } = default!;

        private EtiquetaDTO[]? etiquetas;

        private async Task CargarEtiquetasAsync()
        {
            etiquetas = await Http.GetFromJsonAsync<EtiquetaDTO[]>("api/gestortareas/listetiqueta");
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
