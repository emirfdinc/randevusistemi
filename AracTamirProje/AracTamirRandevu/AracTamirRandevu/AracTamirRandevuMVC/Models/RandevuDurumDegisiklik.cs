using System.ComponentModel.DataAnnotations;

namespace AracTamirRandevuMVC.Models
{
    public class RandevuDurumDegisiklik
    {
        [Key]
        public int DurumDegisiklikID { get; set; }
        public int RandevuID { get; set; }
        public string? EskiDurum { get; set; }
        public string? YeniDurum { get; set; }
        public DateTime DegistirilmeTarihi { get; set; }
        public int DegistirenKisi { get; set; }

    }
}
