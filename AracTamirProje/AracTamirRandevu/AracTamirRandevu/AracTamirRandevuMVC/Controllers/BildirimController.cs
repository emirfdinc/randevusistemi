using Microsoft.AspNetCore.Mvc;
using AracTamirRandevuMVC.Context;
using AracTamirRandevuMVC.Models;

namespace AracTamirRandevuMVC.Controllers
{
    public class BildirimController : Controller
    {
        private readonly AracTamirRandevuDbContext _context;

        public BildirimController(AracTamirRandevuDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.GirisBilgisi = HttpContext.Session.GetString("GirisBilgisi") == "true";

            if (HttpContext.Session.GetInt32("GirisYapmisKullaniciID").HasValue)
            {
                int girisYapmisKullaniciID = HttpContext.Session.GetInt32("GirisYapmisKullaniciID").Value;

                var kullaniciBildirimleri = _context.Bildirimler
                    .Where(n => n.KullaniciID == girisYapmisKullaniciID)
                    .Select(n => new BildirimViewModel
                    {
                        BildirimID = n.BildirimID,
                        BildirimMetni = n.BildirimMetni,
                        OlusturulmaTarihi = n.OlusturulmaTarihi,
                        RandevuTarihi = n.Randevu.RandevuTarihi,
                        Plaka = n.Randevu.Araba.Plaka
                    })
                    .ToList();

                return View(kullaniciBildirimleri);
            }
            else
            {
                return RedirectToAction("Index", "Giris");
            }

        }
    }
}
