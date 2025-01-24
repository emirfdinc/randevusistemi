using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AracTamirRandevuMVC.Models
{
    public class Bildirim
    {
        [Key]
        public int BildirimID { get; set; }

        [ForeignKey("Kullanici")]
        public int KullaniciID { get; set; }

        [ForeignKey("Randevu")]
        public int RandevuID { get; set; }
        public Randevu? Randevu { get; set; }
        public Kullanici? Kullanici { get; set; }
        public string? BildirimMetni { get; set; }
        public bool? OkunduBilgisi { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
