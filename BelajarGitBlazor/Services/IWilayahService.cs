using BelajarGitBlazor.Data.Entities;

namespace BelajarGitBlazor.Services;

public interface IWilayahService
{
    Task<WilayahItem[]> GetProvinsiAsync();
    Task<WilayahItem[]> GetKabupatenAsync(string provinsiCode);
    Task<WilayahItem[]> GetKecamatanAsync(string kabupatenCode);
    Task<WilayahItem[]> GetKelurahanAsync(string kecamatanCode);
}
