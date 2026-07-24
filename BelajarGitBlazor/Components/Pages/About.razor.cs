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
    private bool _isStatsVisible = false;

    // State untuk counter animasi
    private int _statProvinsi = 0;
    private int _statKabupaten = 0;
    private int _statKecamatan = 0;
    private int _statKelurahan = 0;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Task.Delay(300);
            _isChartVisible = true;
            _isStatsVisible = true;
            StateHasChanged();
            
            // Jalankan animasi angka
            await AnimateStats();
        }
    }

    private async Task AnimateStats()
    {
        int steps = 20;
        int targetProvinsi = 38;
        int targetKabupaten = 514;
        int targetKecamatan = 7277;
        int targetKelurahan = 83931;

        for (int i = 1; i <= steps; i++)
        {
            _statProvinsi = (int)Math.Round((double)targetProvinsi * i / steps);
            _statKabupaten = (int)Math.Round((double)targetKabupaten * i / steps);
            _statKecamatan = (int)Math.Round((double)targetKecamatan * i / steps);
            _statKelurahan = (int)Math.Round((double)targetKelurahan * i / steps);
            
            StateHasChanged();
            await Task.Delay(40);
        }
        
        // Pastikan angka target akhir presisi
        _statProvinsi = targetProvinsi;
        _statKabupaten = targetKabupaten;
        _statKecamatan = targetKecamatan;
        _statKelurahan = targetKelurahan;
        
        // Hilangkan efek pop setelah 300ms
        await Task.Delay(300);
        _isStatsVisible = false;
        StateHasChanged();
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
