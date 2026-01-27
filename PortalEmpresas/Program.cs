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
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

// 🔹 Build & Configure Pipeline
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
