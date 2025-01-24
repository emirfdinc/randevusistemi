using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AracTamirRandevuMVC.Models
{
    public class Randevu
    {
        [Key]
        public int randevuID { get; set; }

        [ForeignKey("Araba")]
        public int? ArabaID { get; set; }

        public Araba? Araba { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public string Aciklama { get; set; }
        public string? OnayDurumu { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
    }
}
