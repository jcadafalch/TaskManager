using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GestorTareas.Client.Shared
{
    public partial class AppBar
    {
        private bool _isDarkMode ;

        [Parameter]
        public EventCallback OnSidebarToggled { get; set; }

        [Parameter]
        public EventCallback<bool> OnThemeToggled { get; set; }

        private async Task ToggleTheme()
        {
            _isDarkMode = !_isDarkMode;

            await OnThemeToggled.InvokeAsync(_isDarkMode);
        }
    }
}
