using MudBlazor.Services;
using PortalEmpresas.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PortalEmpresas.Components.Auth;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Blazor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 🔹 MudBlazor UI Library
builder.Services.AddMudServices();

// 🔹 Authentication & Authorization
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

// 🔹 Build & Configure Pipeline
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
