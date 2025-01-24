using System.ComponentModel.DataAnnotations;

namespace AracTamirRandevuMVC.Models
{
    public class Kullanici
    {
        [Key]
        public int KullaniciID { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz!")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz!")]
        public string Sifre { get; set; }
        public string? Email { get; set; }
        public string? TelefonNo { get; set; }
    }
}
