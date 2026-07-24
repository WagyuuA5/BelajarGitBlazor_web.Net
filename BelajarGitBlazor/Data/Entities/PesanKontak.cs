namespace BelajarGitBlazor.Data.Entities;

public class PesanKontak
{
    public int Id { get; set; }
    public string NamaPengirim { get; set; } = "";
    public string EmailPengirim { get; set; } = "";
    public string Isi { get; set; } = "";
    public DateTime WaktuKirim { get; set; } = DateTime.Now;
    public StatusPesan Status { get; set; } = StatusPesan.BelumDibaca;
    public string? UserId { get; set; } // opsional, jika pengirim adalah user yang login
}

public enum StatusPesan
{
    BelumDibaca,
    SudahDibaca,
    SudahDitanggapi
}
