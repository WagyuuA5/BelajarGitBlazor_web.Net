using BelajarGitBlazor.Models;

namespace BelajarGitBlazor.Services;

/// <summary>
/// Mock implementation of IWilayahService untuk pengembangan UI Wahyu.
/// Digunakan saat #if DEBUG aktif — tidak memerlukan koneksi database atau API eksternal.
/// Swap ke WilayahService (implementasi nyata partner) untuk build production.
/// </summary>
public class MockWilayahService : IWilayahService
{
    // ===== Data Dummy Provinsi =====
    private static readonly WilayahItem[] _provinsi = new[]
    {
        new WilayahItem { Code = "11", Name = "Aceh" },
        new WilayahItem { Code = "12", Name = "Sumatera Utara" },
        new WilayahItem { Code = "13", Name = "Sumatera Barat" },
        new WilayahItem { Code = "31", Name = "DKI Jakarta" },
        new WilayahItem { Code = "32", Name = "Jawa Barat" },
        new WilayahItem { Code = "33", Name = "Jawa Tengah" },
        new WilayahItem { Code = "34", Name = "DI Yogyakarta" },
        new WilayahItem { Code = "35", Name = "Jawa Timur" },
        new WilayahItem { Code = "36", Name = "Banten" },
        new WilayahItem { Code = "51", Name = "Bali" },
        new WilayahItem { Code = "52", Name = "Nusa Tenggara Barat" },
        new WilayahItem { Code = "53", Name = "Nusa Tenggara Timur" },
        new WilayahItem { Code = "61", Name = "Kalimantan Barat" },
        new WilayahItem { Code = "64", Name = "Kalimantan Timur" },
        new WilayahItem { Code = "71", Name = "Sulawesi Utara" },
        new WilayahItem { Code = "73", Name = "Sulawesi Selatan" },
    };

    // ===== Data Dummy Kabupaten per Provinsi =====
    private static readonly Dictionary<string, WilayahItem[]> _kabupaten = new()
    {
        ["11"] = new[]
        {
            new WilayahItem { Code = "11.01", Name = "Kabupaten Simeulue" },
            new WilayahItem { Code = "11.02", Name = "Kabupaten Aceh Singkil" },
            new WilayahItem { Code = "11.71", Name = "Kota Banda Aceh" },
        },
        ["12"] = new[]
        {
            new WilayahItem { Code = "12.01", Name = "Kabupaten Nias" },
            new WilayahItem { Code = "12.07", Name = "Kabupaten Deli Serdang" },
            new WilayahItem { Code = "12.71", Name = "Kota Medan" },
        },
        ["13"] = new[]
        {
            new WilayahItem { Code = "13.01", Name = "Kabupaten Kepulauan Mentawai" },
            new WilayahItem { Code = "13.71", Name = "Kota Padang" },
            new WilayahItem { Code = "13.72", Name = "Kota Solok" },
        },
        ["31"] = new[]
        {
            new WilayahItem { Code = "31.01", Name = "Kabupaten Kepulauan Seribu" },
            new WilayahItem { Code = "31.71", Name = "Kota Jakarta Selatan" },
            new WilayahItem { Code = "31.72", Name = "Kota Jakarta Timur" },
            new WilayahItem { Code = "31.73", Name = "Kota Jakarta Pusat" },
            new WilayahItem { Code = "31.74", Name = "Kota Jakarta Barat" },
            new WilayahItem { Code = "31.75", Name = "Kota Jakarta Utara" },
        },
        ["32"] = new[]
        {
            new WilayahItem { Code = "32.01", Name = "Kabupaten Bogor" },
            new WilayahItem { Code = "32.04", Name = "Kabupaten Bandung" },
            new WilayahItem { Code = "32.16", Name = "Kabupaten Bekasi" },
            new WilayahItem { Code = "32.71", Name = "Kota Bogor" },
            new WilayahItem { Code = "32.73", Name = "Kota Bandung" },
            new WilayahItem { Code = "32.75", Name = "Kota Bekasi" },
            new WilayahItem { Code = "32.76", Name = "Kota Depok" },
        },
        ["33"] = new[]
        {
            new WilayahItem { Code = "33.01", Name = "Kabupaten Cilacap" },
            new WilayahItem { Code = "33.02", Name = "Kabupaten Banyumas" },
            new WilayahItem { Code = "33.74", Name = "Kota Semarang" },
        },
        ["34"] = new[]
        {
            new WilayahItem { Code = "34.01", Name = "Kabupaten Kulon Progo" },
            new WilayahItem { Code = "34.02", Name = "Kabupaten Bantul" },
            new WilayahItem { Code = "34.71", Name = "Kota Yogyakarta" },
        },
        ["35"] = new[]
        {
            new WilayahItem { Code = "35.01", Name = "Kabupaten Pacitan" },
            new WilayahItem { Code = "35.78", Name = "Kota Surabaya" },
            new WilayahItem { Code = "35.79", Name = "Kota Batu" },
        },
        ["36"] = new[]
        {
            new WilayahItem { Code = "36.01", Name = "Kabupaten Pandeglang" },
            new WilayahItem { Code = "36.71", Name = "Kota Tangerang" },
            new WilayahItem { Code = "36.74", Name = "Kota Tangerang Selatan" },
        },
        ["51"] = new[]
        {
            new WilayahItem { Code = "51.01", Name = "Kabupaten Jembrana" },
            new WilayahItem { Code = "51.02", Name = "Kabupaten Tabanan" },
            new WilayahItem { Code = "51.71", Name = "Kota Denpasar" },
        },
        ["52"] = new[]
        {
            new WilayahItem { Code = "52.01", Name = "Kabupaten Lombok Barat" },
            new WilayahItem { Code = "52.71", Name = "Kota Mataram" },
        },
        ["53"] = new[]
        {
            new WilayahItem { Code = "53.01", Name = "Kabupaten Sumba Barat" },
            new WilayahItem { Code = "53.71", Name = "Kota Kupang" },
        },
        ["61"] = new[]
        {
            new WilayahItem { Code = "61.01", Name = "Kabupaten Sambas" },
            new WilayahItem { Code = "61.71", Name = "Kota Pontianak" },
        },
        ["64"] = new[]
        {
            new WilayahItem { Code = "64.01", Name = "Kabupaten Paser" },
            new WilayahItem { Code = "64.72", Name = "Kota Balikpapan" },
            new WilayahItem { Code = "64.74", Name = "Kota Samarinda" },
        },
        ["71"] = new[]
        {
            new WilayahItem { Code = "71.01", Name = "Kabupaten Bolaang Mongondow" },
            new WilayahItem { Code = "71.71", Name = "Kota Manado" },
        },
        ["73"] = new[]
        {
            new WilayahItem { Code = "73.01", Name = "Kabupaten Selayar" },
            new WilayahItem { Code = "73.71", Name = "Kota Makassar" },
        },
    };

    // ===== Data Dummy Kecamatan per Kabupaten =====
    private static readonly Dictionary<string, WilayahItem[]> _kecamatan = new()
    {
        ["32.01"] = new[]
        {
            new WilayahItem { Code = "32.01.01", Name = "Kecamatan Nanggung" },
            new WilayahItem { Code = "32.01.02", Name = "Kecamatan Leuwiliang" },
            new WilayahItem { Code = "32.01.03", Name = "Kecamatan Leuwisadeng" },
        },
        ["32.73"] = new[]
        {
            new WilayahItem { Code = "32.73.01", Name = "Kecamatan Bandung Kulon" },
            new WilayahItem { Code = "32.73.02", Name = "Kecamatan Babakan Ciparay" },
            new WilayahItem { Code = "32.73.03", Name = "Kecamatan Bojongloa Kaler" },
            new WilayahItem { Code = "32.73.04", Name = "Kecamatan Astanaanyar" },
        },
        ["32.76"] = new[]
        {
            new WilayahItem { Code = "32.76.01", Name = "Kecamatan Sawangan" },
            new WilayahItem { Code = "32.76.02", Name = "Kecamatan Bojongsari" },
            new WilayahItem { Code = "32.76.03", Name = "Kecamatan Pancoran Mas" },
        },
        ["31.71"] = new[]
        {
            new WilayahItem { Code = "31.71.01", Name = "Kecamatan Tebet" },
            new WilayahItem { Code = "31.71.02", Name = "Kecamatan Setiabudi" },
            new WilayahItem { Code = "31.71.03", Name = "Kecamatan Mampang Prapatan" },
        },
        ["31.73"] = new[]
        {
            new WilayahItem { Code = "31.73.01", Name = "Kecamatan Gambir" },
            new WilayahItem { Code = "31.73.02", Name = "Kecamatan Sawah Besar" },
            new WilayahItem { Code = "31.73.03", Name = "Kecamatan Kemayoran" },
        },
        ["51.71"] = new[]
        {
            new WilayahItem { Code = "51.71.01", Name = "Kecamatan Denpasar Selatan" },
            new WilayahItem { Code = "51.71.02", Name = "Kecamatan Denpasar Timur" },
            new WilayahItem { Code = "51.71.03", Name = "Kecamatan Denpasar Barat" },
            new WilayahItem { Code = "51.71.04", Name = "Kecamatan Denpasar Utara" },
        },
    };

    // ===== Data Dummy Kelurahan per Kecamatan =====
    private static readonly Dictionary<string, WilayahItem[]> _kelurahan = new()
    {
        ["32.73.01"] = new[]
        {
            new WilayahItem { Code = "32.73.01.1001", Name = "Kelurahan Gempol Sari" },
            new WilayahItem { Code = "32.73.01.1002", Name = "Kelurahan Cigondewah Kaler" },
            new WilayahItem { Code = "32.73.01.1003", Name = "Kelurahan Warung Muncang" },
        },
        ["32.73.02"] = new[]
        {
            new WilayahItem { Code = "32.73.02.1001", Name = "Kelurahan Babakan" },
            new WilayahItem { Code = "32.73.02.1002", Name = "Kelurahan Babakan Ciparay" },
            new WilayahItem { Code = "32.73.02.1003", Name = "Kelurahan Sukahaji" },
        },
        ["32.76.01"] = new[]
        {
            new WilayahItem { Code = "32.76.01.1001", Name = "Kelurahan Sawangan" },
            new WilayahItem { Code = "32.76.01.1002", Name = "Kelurahan Sawangan Baru" },
            new WilayahItem { Code = "32.76.01.1003", Name = "Kelurahan Pengasinan" },
        },
        ["31.71.01"] = new[]
        {
            new WilayahItem { Code = "31.71.01.1001", Name = "Kelurahan Tebet Barat" },
            new WilayahItem { Code = "31.71.01.1002", Name = "Kelurahan Tebet Timur" },
            new WilayahItem { Code = "31.71.01.1003", Name = "Kelurahan Kebon Baru" },
            new WilayahItem { Code = "31.71.01.1004", Name = "Kelurahan Bukit Duri" },
        },
        ["31.73.01"] = new[]
        {
            new WilayahItem { Code = "31.73.01.1001", Name = "Kelurahan Gambir" },
            new WilayahItem { Code = "31.73.01.1002", Name = "Kelurahan Cideng" },
            new WilayahItem { Code = "31.73.01.1003", Name = "Kelurahan Petojo Utara" },
        },
        ["51.71.01"] = new[]
        {
            new WilayahItem { Code = "51.71.01.1001", Name = "Kelurahan Sanur Kauh" },
            new WilayahItem { Code = "51.71.01.1002", Name = "Kelurahan Sanur" },
            new WilayahItem { Code = "51.71.01.1003", Name = "Kelurahan Sesetan" },
        },
    };

    // ===== Fallback generator untuk kabupaten/kecamatan/kelurahan yang belum didata =====
    private static WilayahItem[] GenerateFallback(string parentCode, string level, int count = 3)
    {
        return Enumerable.Range(1, count).Select(i =>
            new WilayahItem
            {
                Code = $"{parentCode}.{i:D2}",
                Name = $"{level} Contoh {i} ({parentCode})"
            }).ToArray();
    }

    // ===== Interface Implementation =====

    public Task<WilayahItem[]> GetProvinsiAsync()
        => Task.FromResult(_provinsi);

    public Task<WilayahItem[]> GetKabupatenAsync(string provinsiCode)
    {
        var result = _kabupaten.TryGetValue(provinsiCode, out var list)
            ? list
            : GenerateFallback(provinsiCode, "Kabupaten", 3);
        return Task.FromResult(result);
    }

    public Task<WilayahItem[]> GetKecamatanAsync(string kabupatenCode)
    {
        var result = _kecamatan.TryGetValue(kabupatenCode, out var list)
            ? list
            : GenerateFallback(kabupatenCode, "Kecamatan", 3);
        return Task.FromResult(result);
    }

    public Task<WilayahItem[]> GetKelurahanAsync(string kecamatanCode)
    {
        var result = _kelurahan.TryGetValue(kecamatanCode, out var list)
            ? list
            : GenerateFallback(kecamatanCode, "Kelurahan", 4);
        return Task.FromResult(result);
    }
}
