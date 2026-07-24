using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BelajarGitBlazor.Services;

/// <summary>
/// Custom AuthenticationStateProvider yang terhubung ke AuthService (in-memory).
/// Ini memungkinkan penggunaan komponen Blazor standar seperti <AuthorizeView> dan [Authorize].
/// </summary>
public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthService _authService;

    public CustomAuthStateProvider(AuthService authService)
    {
        _authService = authService;
        // Dengarkan perubahan dari AuthService dan notifikasi Blazor auth system
        _authService.OnAuthStateChanged += NotifyAuthStateChanged;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_authService.IsAuthenticated && _authService.CurrentUser is not null)
        {
            var user = _authService.CurrentUser;
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };
            var identity = new ClaimsIdentity(claims, "CustomAuth");
            var principal = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(principal));
        }

        // User belum login — kembalikan anonymous state
        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
    }

    private void NotifyAuthStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
