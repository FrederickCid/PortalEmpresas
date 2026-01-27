using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PortalEmpresas.Components.Auth;

namespace PortalEmpresas.Components.Layout
{
    public partial class MainLayout : IDisposable
    {
        private bool _drawerOpen;

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } 
      

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
