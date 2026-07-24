using BelajarGitBlazor.Services;
using BelajarGitBlazor.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://wilayah.id/") });

// Contract-first development: MockWilayahService dipakai saat DEBUG (UI Wahyu)
// WilayahService (implementasi nyata partner) aktif saat build production.
#if DEBUG
builder.Services.AddScoped<IWilayahService, MockWilayahService>();
#else
builder.Services.AddScoped<IWilayahService, WilayahService>();
#endif


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();