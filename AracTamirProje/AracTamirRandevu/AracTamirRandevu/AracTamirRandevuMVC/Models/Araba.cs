using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AracTamirRandevuMVC.Models
{
    public class Araba
    {
        [Key]
        public int ArabaID { get; set; }

        [ForeignKey("Kullanici")]
        public int? KullaniciID { get; set; }

        public Kullanici? Kullanici { get; set; }
        public string? Marka { get; set; }
        public string? Model { get; set; }
        public int Yil { get; set; }
        public string? Plaka { get; set; }

    }
}
