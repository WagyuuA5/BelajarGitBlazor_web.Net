namespace BelajarGitBlazor.Services;

public class AuthService
{
    private readonly List<UserRecord> _users = new()
    {
        new UserRecord("Admin", "admin@belajargit.dev", BCryptHash("Admin@123")),
        new UserRecord("Demo User", "demo@belajargit.dev", BCryptHash("Demo@1234")),
    };

    private UserRecord? _currentUser;

    public UserRecord? CurrentUser => _currentUser;
    public bool IsAuthenticated => _currentUser is not null;

    public Task<AuthResult> LoginAsync(string email, string password)
    {
        var user = _users.FirstOrDefault(u =>
            u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (user is null)
            return Task.FromResult(new AuthResult(false, "Email tidak ditemukan."));

        if (!VerifyHash(password, user.PasswordHash))
            return Task.FromResult(new AuthResult(false, "Password salah."));

        _currentUser = user;
        return Task.FromResult(new AuthResult(true, "Login berhasil!"));
    }

    public Task<AuthResult> RegisterAsync(string name, string email, string password)
    {
        if (_users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            return Task.FromResult(new AuthResult(false, "Email sudah terdaftar."));

        var newUser = new UserRecord(name, email, BCryptHash(password));
        _users.Add(newUser);
        _currentUser = newUser;
        return Task.FromResult(new AuthResult(true, "Registrasi berhasil! Selamat datang."));
    }

    public void Logout() => _currentUser = null;

    private static string BCryptHash(string plain) =>
        Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plain + "_salted"));

    private static bool VerifyHash(string plain, string hash) =>
        BCryptHash(plain) == hash;

    public record UserRecord(string Name, string Email, string PasswordHash);
    public record AuthResult(bool Success, string Message);
}
