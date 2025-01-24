using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AracTamirRandevuMVC.Context;
using AracTamirRandevuMVC.Models;

namespace AracTamirRandevuMVC.Controllers
{
    public class RandevuController : Controller
    {
        private readonly AracTamirRandevuDbContext _context;

        public RandevuController(AracTamirRandevuDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.GirisBilgisi = HttpContext.Session.GetString("GirisBilgisi") == "true";
            return View();
        }

        [HttpPost]
        public ActionResult Index(Araba Araba)
        {
            ViewBag.GirisBilgisi = HttpContext.Session.GetString("" +
                "girisBilgisi") == "true";
            int? girisYapmisKullaniciID = HttpContext.Session.GetInt32("GirisYapmisKullaniciID");

            Araba.KullaniciID = girisYapmisKullaniciID;


            if (ModelState.IsValid)
            {
                _context.Arabalar.Add(Araba);
                _context.SaveChanges();
                TempData["CarSuccessMessage"] = "Arabanız başarıyla eklendi. Randevu alabilirsiniz.";
                return RedirectToAction("RandevuEkle");
            }

            return View(Araba);

        }

        public IActionResult RandevuEkle() 
        {
            ViewBag.girisBilgisi = HttpContext.Session.GetString("GirisBilgisi") == "true";
            int? girisYapmisKullaniciID = HttpContext.Session.GetInt32("GirisYapmisKullaniciID");

            var arabalar = _context.Arabalar
         .Where(c => c.KullaniciID == girisYapmisKullaniciID.Value)
         .Select(c => new SelectListItem
         {
             Value = c.ArabaID.ToString(),
             Text = c.Plaka
         })
         .ToList();

            ViewBag.Arabalar = arabalar;

            return View();
        }

        [HttpPost]
        public IActionResult RandevuEkle(Randevu randevu)
        {
            ViewBag.girisBilgisi = HttpContext.Session.GetString("girisBilgisi") == "true";
            int? girisYapmisKullaniciID = HttpContext.Session.GetInt32("GirisYapmisKullaniciID");

            randevu.OlusturulmaTarihi= DateTime.Now;
            randevu.OnayDurumu = "Onay bekliyor";
            if (ModelState.IsValid)
            {
                _context.Randevular.Add(randevu);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Randevu isteğiniz başarıyla gönderildi. Admin işlem yaptıktan sonra randevularım veya bildirim kısmından randevu durumunu öğrenebilirsiniz.";
                return RedirectToAction("RandevuEkle");
            }
            
            var arabalar = _context.Arabalar
           .Where(c => c.KullaniciID == girisYapmisKullaniciID.Value)
           .Select(c => new SelectListItem
           {
               Value = c.ArabaID.ToString(),
               Text = c.Plaka
           })
           .ToList();

            ViewBag.Arabalar = arabalar;

            return View(randevu);
        }
    }
}
