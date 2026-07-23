using Microsoft.AspNetCore.Components;

namespace BelajarGitBlazor.Components.Pages;

public partial class About : ComponentBase
{
    // Data model untuk grafik batang horizontal
    private List<CoverageData> CoverageItems { get; set; } = new()
    {
        new("Provinsi",        38,      "bg-primary"),
        new("Kab/Kota",        514,     "bg-warning"),
        new("Kecamatan",       7_277,   "bg-success"),
        new("Desa/Kelurahan",  83_931,  "bg-accent"),
    };

    // Properti untuk mengontrol animasi bar chart
    private bool _isChartVisible = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Tunda sedikit agar transisi CSS terlihat (bar dari 0% → target)
            await Task.Delay(300);
            _isChartVisible = true;
            StateHasChanged();
        }
    }

    /// <summary>
    /// Menghitung lebar persentase bar relatif terhadap nilai terbesar.
    /// Menggunakan skala logaritmik agar bar terkecil (38) tetap terlihat proporsional.
    /// </summary>
    private string GetBarWidth(int value)
    {
        if (!_isChartVisible) return "0%";

        int maxValue = CoverageItems.Max(c => c.Value);
        // Skala logaritmik: log(value) / log(max) * 100
        double logWidth = Math.Log(value) / Math.Log(maxValue) * 100;
        // Minimum 8% agar label tetap terbaca
        double width = Math.Max(logWidth, 8);
        return $"{width:F1}%";
    }

    /// <summary>
    /// Format angka dengan pemisah ribuan titik (gaya Indonesia).
    /// </summary>
    private static string FormatNumber(int number)
    {
        return number.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
    }

    // Record untuk data cakupan wilayah
    public record CoverageData(string Label, int Value, string ColorClass);
}
