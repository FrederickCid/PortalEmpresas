using MudBlazor.Services;
using PortalEmpresas.Components;
using PortalEmpresas.Components.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Razor Components (Server)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 🔹 MudBlazor
builder.Services.AddMudServices();

// 🔹 HttpContext
builder.Services.AddHttpContextAccessor();

// 🔹 Authentication (OBLIGATORIO)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/not-authorized";
    });

// 🔹 Authorization (NO Core)
builder.Services.AddAuthorization();

// 🔹 AuthStateProvider
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<AuthStateProvider>());

var app = builder.Build();

// 🔹 Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔥 CLAVE
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
