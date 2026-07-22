namespace BelajarGitBlazor.Services;

public class ThemeService
{
    private string _theme = "light";

    public string Current => _theme;

    public event Action? OnChanged;

    public void Initialize(string stored)
    {
        if (!string.IsNullOrWhiteSpace(stored) && stored != _theme)
            _theme = stored;
    }

    public void Set(string theme)
    {
        if (_theme == theme) return;
        _theme = theme;
        OnChanged?.Invoke();
    }
}
