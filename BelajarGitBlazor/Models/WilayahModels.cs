namespace BelajarGitBlazor.Models;

public class WilayahItem
{
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";
}

public class WilayahResponse
{
    public WilayahItem[] Data { get; set; } = Array.Empty<WilayahItem>();
}
