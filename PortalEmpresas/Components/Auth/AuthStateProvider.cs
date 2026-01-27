using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace PortalEmpresas.Components.Auth
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous =
            new ClaimsPrincipal(new ClaimsIdentity());

        private ClaimsPrincipal _currentUser;

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(
                new AuthenticationState(_currentUser ?? _anonymous)
            );
        }

        public void SignIn(string rutEmpresa, string email)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim("RutEmpresa", rutEmpresa)
            }, "FakeAuth");

            _currentUser = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void SignOut()
        {
            _currentUser = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
