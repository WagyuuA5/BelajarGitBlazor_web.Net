using BelajarGitBlazor.Data.Entities;

namespace BelajarGitBlazor.Services;

public interface IPesanKontakService
{
    Task KirimPesanAsync(PesanKontak pesan);
    Task<List<PesanKontak>> GetSemuaPesanAsync();
    Task<int> HitungPesanBelumDibacaAsync();
    Task TandaiSudahDibacaAsync(int pesanId);
    Task TandaiSudahDitanggapiAsync(int pesanId);
}
