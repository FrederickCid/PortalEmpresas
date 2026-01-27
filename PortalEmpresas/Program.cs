using MudBlazor.Services;
using PortalEmpresas.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PortalEmpresas.Components.Auth;



var builder = WebApplication.CreateBuilder(args);

// 🔹 Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 🔹 MudBlazor
builder.Services.AddMudServices();
//authProvider
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

var app = builder.Build();

// 🔹 Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
