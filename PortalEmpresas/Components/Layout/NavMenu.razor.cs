using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PortalAG_V2.Services;
using PortalEmpresas.Components.Auth;

namespace PortalEmpresas.Components.Layout
{
    public partial class NavMenu
    {
        [Inject] AuthStateProvider AuthState { get; set; }
        [Inject] NavigationManager Nav { get; set; }
        [Inject] IJSRuntime js { get; set; }


        bool _drawerOpen = true;
        private void ToggleDrawer()
        {
            _drawerOpen = !_drawerOpen;
            StateHasChanged();
        }

        private async Task HandleLogout()
        {
            // 🧹 borrar cookies
            await js.DeleteCookie("access_token");
            await js.DeleteCookie("refresh_token");

            // 🔓 limpiar estado
            AuthState.ClearUser();

            // 🔁 redirigir
            Nav.NavigateTo("/login", forceLoad: true);
        }
    }
}
