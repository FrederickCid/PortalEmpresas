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
// 1️⃣ Registrar el concreto
builder.Services.AddScoped<AuthStateProvider>();

// 2️⃣ Registrar el contrato apuntando al concreto
builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<AuthStateProvider>());
// 🔹 Build & Configure Pipeline
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
