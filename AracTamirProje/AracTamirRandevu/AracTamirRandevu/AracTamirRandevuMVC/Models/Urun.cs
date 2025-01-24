using System.ComponentModel.DataAnnotations;

namespace AracTamirRandevuMVC.Models
{
    public class Urun
    {
        [Key]
        public int UrunID { get; set; }
        public string? Ad { get; set; }
        public string? Aciklama { get; set; }
        public decimal Fiyat { get; set; }
        public int Stok { get; set; }
        public string? ResimDosyaYolu { get; set;}
    }
}
