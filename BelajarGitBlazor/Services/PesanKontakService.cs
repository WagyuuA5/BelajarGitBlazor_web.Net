using BelajarGitBlazor.Data;
using BelajarGitBlazor.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BelajarGitBlazor.Services;

public class PesanKontakService : IPesanKontakService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public PesanKontakService(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task KirimPesanAsync(PesanKontak pesan)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        pesan.WaktuKirim = DateTime.Now;
        db.PesanKontak.Add(pesan);
        await db.SaveChangesAsync();
    }

    public async Task<List<PesanKontak>> GetSemuaPesanAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.PesanKontak
            .OrderByDescending(p => p.WaktuKirim)
            .ToListAsync();
    }

    public async Task<int> HitungPesanBelumDibacaAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.PesanKontak
            .CountAsync(p => p.Status == StatusPesan.BelumDibaca);
    }

    public async Task TandaiSudahDibacaAsync(int pesanId)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        var pesan = await db.PesanKontak.FindAsync(pesanId);
        if (pesan is not null)
        {
            pesan.Status = StatusPesan.SudahDibaca;
            await db.SaveChangesAsync();
        }
    }

    public async Task TandaiSudahDitanggapiAsync(int pesanId)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        var pesan = await db.PesanKontak.FindAsync(pesanId);
        if (pesan is not null)
        {
            pesan.Status = StatusPesan.SudahDitanggapi;
            await db.SaveChangesAsync();
        }
    }
}
