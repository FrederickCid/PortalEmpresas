using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PortalEmpresas.Components.Auth
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous =
            new ClaimsPrincipal(new ClaimsIdentity());

        private ClaimsPrincipal _currentUser;

        public AuthStateProvider()
        {
            _currentUser = _anonymous;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }

        // 🔑 LOGIN: crear usuario desde JWT
        public void SetUserFromToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var identity = new ClaimsIdentity(
                token.Claims,
                authenticationType: "jwt"
            );

            _currentUser = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        // 🔓 LOGOUT
        public void ClearUser()
        {
            _currentUser = _anonymous;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
