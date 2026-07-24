using BelajarGitBlazor.Data;
using BelajarGitBlazor.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BelajarGitBlazor.Services;

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

        if (user == null)
            return new AuthResult(false, "Username tidak ditemukan.", null);

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return new AuthResult(false, "Password salah.", null);

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

        return new AuthResult(true, "Registrasi berhasil!", newUser);
    }
}

public record AuthResult(bool Success, string Message, User? User);
