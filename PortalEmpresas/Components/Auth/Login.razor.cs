using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PortalAG_V2.Services;
using PortalEmpresas.Shared.Models.Login;
using PortalEmpresas.Shared.Services.Login;

namespace PortalEmpresas.Components.Auth
{
    public partial class Login
    {
        [Inject] LoginData LoginData { get; set; }
        [Inject] IJSRuntime js { get; set; }
        [Inject] AuthStateProvider AuthState { get; set; }
        [Inject] NavigationManager Nav { get; set; }
        private LoginModel loginModel = new();
        private bool loading = false;
        private bool showPassword = false;
        private EditContext editContext;
        private string? error;

        protected override void OnInitialized()
        {
            editContext = new EditContext(loginModel);
        }

        private async Task HandleLogin()
        {
            if (!editContext.Validate())
                return;

            loading = true;
            StateHasChanged();

            var (response, status) = await LoginData.LoginPost(
                loginModel.Email,
                loginModel.Password,
                loginModel.RutEmpresa
            );

            loading = false;

            if (!response.Success || response.Data == null)
            {
                // aquí puedes mostrar snackbar o error
                return;
            }

            // 1️⃣ Guardar token en cookie (JS)
            await js.WriteCookie("access_token", response.Data.token, 1);
            await js.WriteCookie("refresh_token", response.Data.refreshToken, 7);

            // 2️⃣ Autenticar en Blazor
            AuthState.SetUserFromToken(response.Data.token);

            // 3️⃣ Navegar
            Nav.NavigateTo("/");
        }

    }
}
