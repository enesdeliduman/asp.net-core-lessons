using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Data
{
    public class Ogrenci
    {
        [Key]
        public int OgrenciKimlik { get; set; }

        [Required(ErrorMessage = "Lütfen öğrenci adını giriniz")]
        public string? OgrenciAd { get; set; }

        [Required(ErrorMessage = "Lütfen öğrenci soyadını giriniz")]
        public string? OgrenciSoyad { get; set; }

        [Display(Name = "Öğrenci ad soyad")]
        public string? AdSoyad
        {
            get
            {
                return this.OgrenciAd + " " + OgrenciSoyad;
            }
        }

        [Required(ErrorMessage = "Lütfen eposta adresini giriniz")]
        [EmailAddress(ErrorMessage = "Geçerli bir eposta adresi girin.")]
        public string? Eposta { get; set; }

        [Required(ErrorMessage = "Lütfen telefon numarasını giriniz")]
        public string? Telefon { get; set; }
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();
    }
}