using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Data{

public class KursKayit
{
    [Key]
    public int KayitId { get; set; }

    [Display(Name = "Öğrenci")]
    public int OgrenciId { get; set; }
    public Ogrenci Ogrenci { get; set; } = null!;

    [Display(Name = "Kurs")]
    public int KursId { get; set; }
    public Kurs Kurs { get; set; } = null!;

    public DateTime KayitTarihi { get; set; } = DateTime.Now;
}
}