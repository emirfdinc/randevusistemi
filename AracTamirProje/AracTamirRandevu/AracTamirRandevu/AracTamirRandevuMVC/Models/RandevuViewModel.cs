namespace AracTamirRandevuMVC.Models
{
    public class RandevuViewModel
    {
        public int RandevuID { get; set; }
        public int ArabaID { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public string Aciklama { get; set; }
        public string OnayDurumu { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public string KullaniciAdi { get; set; }
    }
}
