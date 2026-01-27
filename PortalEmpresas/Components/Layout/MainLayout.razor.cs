using MudBlazor;

namespace PortalEmpresas.Components.Layout
{
    public partial class MainLayout
    {
        private bool _drawerOpen;

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
