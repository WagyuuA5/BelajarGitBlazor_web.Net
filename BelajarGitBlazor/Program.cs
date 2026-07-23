using BelajarGitBlazor.Services;
using BelajarGitBlazor.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://wilayah.id/") });

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();