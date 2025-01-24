namespace AracTamirRandevuMVC.Models
{
    public class BildirimViewModel
    {
        public int BildirimID { get; set; }
        public string BildirimMetni { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public string Plaka { get; set; }
    }
}
