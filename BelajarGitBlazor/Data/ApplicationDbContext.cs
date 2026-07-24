using BelajarGitBlazor.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BelajarGitBlazor.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PesanKontak> PesanKontak => Set<PesanKontak>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PesanKontak>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NamaPengirim).IsRequired().HasMaxLength(100);
            entity.Property(e => e.EmailPengirim).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Isi).IsRequired();
        });
    }
}
