using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AracTamirRandevuMVC.Context;

namespace AracTamirRandevuMVC.Controllers
{
    public class ProfilController : Controller
    {
        private readonly AracTamirRandevuDbContext _context;

        public ProfilController(AracTamirRandevuDbContext context)
        {
            _context = context;
        }

        public IActionResult Randevularim()
        {
            ViewBag.girisBilgisi = HttpContext.Session.GetString("GirisBilgisi") == "true";
            if (HttpContext.Session.GetInt32("GirisYapmisKullaniciID").HasValue)
            {
                int? girisYapmisKullaniciID = HttpContext.Session.GetInt32("GirisYapmisKullaniciID");
                if (girisYapmisKullaniciID.HasValue)
                {
                    var onaylananRandevular = _context.Randevular
                        .Include(a => a.Araba)
                        .Where(a => a.Araba.KullaniciID == girisYapmisKullaniciID.Value && (a.OnayDurumu == "Onaylandı" || a.OnayDurumu=="İptal Edildi"))
                        .ToList();

                    return View(onaylananRandevular);
                }
            }

            return RedirectToAction("Index", "Giris");
        }

        [HttpPost]
        public async Task<IActionResult> RandevuIptal(int randevuID)
        {
            var randevu = await _context.Randevular.FindAsync(randevuID);
            if (randevu == null)
            {
                return NotFound();
            }

            // Randevu tarihinden şu anki zamanı çıkararak fark oluştur
            var zamanFarki = randevu.RandevuTarihi - DateTime.Now;

            // Eğer randevu tarihi geçmişse veya fark 24 saatten az ise, randevuyu iptal etmeye izin verme
            if (zamanFarki.TotalHours < 24)
            {
                TempData["ErrorMessage"] = "Randevuyu son 24 saat içinde iptal edemezsiniz!";
                return RedirectToAction("Randevularim"); // Hata mesajıyla birlikte Randevularim sayfasına yönlendir
            }

            // Randevuyu iptal etme işlemi
            randevu.OnayDurumu = "İptal Edildi"; // Statusu güncelle
            await _context.SaveChangesAsync();

            return RedirectToAction("Randevularim"); // Silme işlemi tamamlandıktan sonra Randevularim sayfasına yönlendir.
        }

        public IActionResult Araclarim()
        {
            ViewBag.GirisBilgisi = HttpContext.Session.GetString("GirisBilgisi") == "true";
            if (HttpContext.Session.GetInt32("GirisYapmisKullaniciID").HasValue)
            {
                int? girisYapmisKullaniciID = HttpContext.Session.GetInt32("GirisYapmisKullaniciID");
                if (girisYapmisKullaniciID.HasValue)
                {
                    var userCars = _context.Arabalar.Where(c => c.KullaniciID== girisYapmisKullaniciID.Value).ToList();
                    return View(userCars);
                }
            }
            return RedirectToAction("Index", "Giris");
        }
    }
}
