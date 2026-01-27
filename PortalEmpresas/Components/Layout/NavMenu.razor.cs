using Microsoft.AspNetCore.Components;
using PortalEmpresas.Components.Auth;

namespace PortalEmpresas.Components.Layout
{
    public partial class NavMenu
    {
        [Inject]
        AuthStateProvider AuthState { get; set; }
        [Inject]
        NavigationManager Nav { get; set; }

        bool _drawerOpen = true;
        private void ToggleDrawer()
        {
            _drawerOpen = !_drawerOpen;
            StateHasChanged();
        }

        private async Task HandleLogout()
        {
            AuthState.SignOut();
            Nav.NavigateTo("/login", forceLoad: true);
        }
    }
}
