﻿using GestorTareas.Client.Components.Dialogs;
using GestorTareas.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Components.Etiqueta;

public partial class EtiquetaComponent
{
    [Inject] IDialogService DialogService { get; set; }
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public EtiquetaDTO Etiqueta { get; set; } = default!;

    [Parameter]
    public bool Modify { get; set; } = default!;

    [Parameter]
    public bool Delete { get; set; } = default!;

    #region Modify
    protected async Task ModifyEtiquetaAsync()
    {
        var parameters = new DialogParameters
        {
            {"Contenido", Etiqueta.Name },
            {"Etiqueta", Etiqueta },
            {"Modify", true}
        };

        var dialog = DialogService.Show<EtiquetaDialog>("Editar", parameters);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            await InvokeAsync(StateHasChanged);
            NavigationManager.NavigateTo("/");
        }
    }
    #endregion

    #region Delete
    protected async Task DeleteEtiquetaAsync()
    {
        var parameters = new DialogParameters
        {
            {"Contenido", "" },
            {"Etiqueta", Etiqueta},
            {"Delete", true }
        };

        var dialog = DialogService.Show<EtiquetaDialog>("¡AVISO!", parameters);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            await InvokeAsync(StateHasChanged);
            NavigationManager.NavigateTo("/");
        }
    }

    #endregion
}