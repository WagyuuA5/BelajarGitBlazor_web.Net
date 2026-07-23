using Microsoft.AspNetCore.Components;

namespace BelajarGitBlazor.Components.Pages;

public partial class About : ComponentBase
{
    private List<CoverageData> CoverageItems { get; set; } = new()
    {
        new("Provinsi",        38,      "bg-primary"),
        new("Kab/Kota",        514,     "bg-warning"),
        new("Kecamatan",       7_277,   "bg-success"),
        new("Desa/Kelurahan",  83_931,  "bg-accent"),
    };

    private bool _isChartVisible = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Task.Delay(300);
            _isChartVisible = true;
            StateHasChanged();
        }
    }

    private string GetBarWidth(int value)
    {
        if (!_isChartVisible) return "0%";

        int maxValue = CoverageItems.Max(c => c.Value);
        double logWidth = Math.Log(value) / Math.Log(maxValue) * 100;
        double width = Math.Max(logWidth, 8);
        return $"{width:F1}%";
    }

    private static string FormatNumber(int number)
    {
        return number.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
    }

    public record CoverageData(string Label, int Value, string ColorClass);
}
