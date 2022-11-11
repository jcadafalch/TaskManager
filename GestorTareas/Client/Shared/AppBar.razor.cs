using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Shared;

/// <summary>
/// Muestra la AppBar
/// </summary>
public partial class AppBar
{
    [Parameter]
    public bool IsDarkMode { get; set; }

    [Parameter]
    public EventCallback OnSidebarToggled { get; set; }

    [Parameter]
    public EventCallback<bool> OnThemeToggled { get; set; }


    /// <summary>
    /// Alterna el tema de la web
    /// </summary>
    private async Task ToggleTheme()
    {
        IsDarkMode = !IsDarkMode;
        await OnThemeToggled.InvokeAsync(IsDarkMode);
    }
}
