using BelajarGitBlazor.Data;
using BelajarGitBlazor.Services;
using BelajarGitBlazor.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// === Razor Components (Blazor Server) ===
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// === Authentication & Authorization ===
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

// === Database (SQLite via EF Core) ===
var connStr = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite(connStr));

// === Application Services ===
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<IPesanKontakService, PesanKontakService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://wilayah.id/") });

// === Wilayah Service (Mock untuk dev, Real untuk production) ===
#if DEBUG
builder.Services.AddScoped<IWilayahService, MockWilayahService>();
#else
builder.Services.AddScoped<IWilayahService, WilayahService>();
#endif

var app = builder.Build();

// === Auto-migrate database saat startup ===
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();