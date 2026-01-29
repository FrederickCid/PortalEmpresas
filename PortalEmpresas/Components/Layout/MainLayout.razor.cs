using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MudBlazor;
using PortalAG_V2.Services;
using PortalEmpresas.Components.Auth;

namespace PortalEmpresas.Components.Layout
{
    public partial class MainLayout : IDisposable
    {
        private bool _drawerOpen;

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject] AuthStateProvider AuthState { get; set; }

        [Inject] IJSRuntime js { get; set; }


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

        protected override async Task OnInitializedAsync()
        {
            AuthenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
            await base.OnInitializedAsync();
        }

        private async void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            AuthenticationStateProvider.AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }
    }
}
