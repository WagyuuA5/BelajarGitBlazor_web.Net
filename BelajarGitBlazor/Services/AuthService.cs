using BelajarGitBlazor.Data;
using BelajarGitBlazor.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BelajarGitBlazor.Services;

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    private User? _currentUser;

    public User? CurrentUser => _currentUser;
    public bool IsAuthenticated => _currentUser is not null;
    public bool IsAdmin => _currentUser?.Role == "Admin";
    public string? UserRole => _currentUser?.Role;

    public event Action? OnAuthStateChanged;

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

        if (user == null)
            return new AuthResult(false, "Username tidak ditemukan.", null);

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return new AuthResult(false, "Password salah.", null);

        _currentUser = user;
        NotifyAuthStateChanged();
        return new AuthResult(true, "Login berhasil!", user);
    }

    public async Task<AuthResult> RegisterAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return new AuthResult(false, "Username dan password tidak boleh kosong.", null);

        if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            return new AuthResult(false, "Username sudah terdaftar.", null);

        var newUser = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = "User"
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        _currentUser = newUser;
        NotifyAuthStateChanged();
        return new AuthResult(true, "Registrasi berhasil!", newUser);
    }

    public void Logout()
    {
        _currentUser = null;
        NotifyAuthStateChanged();
    }

    private void NotifyAuthStateChanged() => OnAuthStateChanged?.Invoke();
}

public record AuthResult(bool Success, string Message, User? User);
