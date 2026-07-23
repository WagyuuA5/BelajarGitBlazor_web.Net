using BelajarGitBlazor.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;

namespace BelajarGitBlazor.Services;

public class WilayahService : IWilayahService
{
    private readonly HttpClient _http;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(6);

    public WilayahService(HttpClient http, IMemoryCache cache)
    {
        _http = http;
        _cache = cache;
    }

    public async Task<WilayahItem[]> GetProvinsiAsync()
    {
        const string cacheKey = "provinsi_list";
        if (!_cache.TryGetValue(cacheKey, out WilayahItem[]? list) || list == null)
        {
            var response = await _http.GetFromJsonAsync<WilayahResponse>("api/provinces.json");
            list = response?.Data ?? Array.Empty<WilayahItem>();
            _cache.Set(cacheKey, list, CacheDuration);
        }
        return list;
    }

    public async Task<WilayahItem[]> GetKabupatenAsync(string provinsiCode)
    {
        string cacheKey = $"kabupaten_{provinsiCode}";
        if (!_cache.TryGetValue(cacheKey, out WilayahItem[]? list) || list == null)
        {
            var response = await _http.GetFromJsonAsync<WilayahResponse>($"api/regencies/{provinsiCode}.json");
            list = response?.Data ?? Array.Empty<WilayahItem>();
            _cache.Set(cacheKey, list, CacheDuration);
        }
        return list;
    }

    public async Task<WilayahItem[]> GetKecamatanAsync(string kabupatenCode)
    {
        string cacheKey = $"kecamatan_{kabupatenCode}";
        if (!_cache.TryGetValue(cacheKey, out WilayahItem[]? list) || list == null)
        {
            var response = await _http.GetFromJsonAsync<WilayahResponse>($"api/districts/{kabupatenCode}.json");
            list = response?.Data ?? Array.Empty<WilayahItem>();
            _cache.Set(cacheKey, list, CacheDuration);
        }
        return list;
    }

    public async Task<WilayahItem[]> GetKelurahanAsync(string kecamatanCode)
    {
        string cacheKey = $"kelurahan_{kecamatanCode}";
        if (!_cache.TryGetValue(cacheKey, out WilayahItem[]? list) || list == null)
        {
            var response = await _http.GetFromJsonAsync<WilayahResponse>($"api/villages/{kecamatanCode}.json");
            list = response?.Data ?? Array.Empty<WilayahItem>();
            _cache.Set(cacheKey, list, CacheDuration);
        }
        return list;
    }
}
