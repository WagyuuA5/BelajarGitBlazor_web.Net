using BelajarGitBlazor.Services;
using BelajarGitBlazor.Components;
using Microsoft.EntityFrameworkCore;
using BelajarGitBlazor.Data;
using BelajarGitBlazor.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Konfigurasi Database SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=app.db"));

// Konfigurasi Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "WilayahKuAuth";
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapPost("/api/auth/login", async (HttpContext context, [Microsoft.AspNetCore.Mvc.FromForm] string username, [Microsoft.AspNetCore.Mvc.FromForm] string password, [Microsoft.AspNetCore.Mvc.FromForm] bool rememberMe, AuthService authService) =>
{
    var result = await authService.LoginAsync(username, password);
    if (result.Success && result.User != null)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, result.User.Username),
            new Claim(ClaimTypes.Role, result.User.Role)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = rememberMe
        };

        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        return Results.Redirect("/");
    }
    return Results.Redirect($"/login?error={Uri.EscapeDataString(result.Message)}");
});

app.MapPost("/api/auth/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});

// Data Seeding untuk Admin
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Pastikan DB terbuat

    if (!dbContext.Users.Any(u => u.Role == "Admin"))
    {
        dbContext.Users.Add(new User
        {
            Username = "admin_wilayahku",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("adminpassword123"),
            Role = "Admin"
        });
        dbContext.SaveChanges();
    }
}

app.Run();