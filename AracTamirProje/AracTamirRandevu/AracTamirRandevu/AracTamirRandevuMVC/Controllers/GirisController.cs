using Microsoft.AspNetCore.Mvc;
using AracTamirRandevuMVC.Context;
using AracTamirRandevuMVC.Models;

namespace AracTamirRandevuMVC.Controllers
{
    public class GirisController : Controller
    {
        private readonly AracTamirRandevuDbContext _context;

        public GirisController(AracTamirRandevuDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Kullanici kullanici)
        {

            if (ModelState.IsValid)
            { 
                var kisi = _context.Kullanicilar.SingleOrDefault(u => u.KullaniciAdi == kullanici.KullaniciAdi && u.Sifre == kullanici.Sifre);
                if (kisi != null) {
                    var GirisYapmisKullaniciID = kisi.KullaniciID;
                    HttpContext.Session.SetInt32("GirisYapmisKullaniciID", GirisYapmisKullaniciID);
                    HttpContext.Session.SetString("KullaniciAdi", kisi.KullaniciAdi);
                    HttpContext.Session.SetString("GirisBilgisi", "true");
                    return RedirectToAction("Index","Anasayfa");
                }
            else
            {
                    TempData["ErrorMessage"] = "Kullanıcı adı veya şifre yanlış"; //Giriş yapmış kullanıcı yoksa
                    return RedirectToAction("Index","Giris");
                }
            }
         
            return View(kullanici);
        }

        public IActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public IActionResult KayitOl(Kullanici kullanici)
        {
            
            _context.Kullanicilar.Add(kullanici);
            _context.SaveChanges();
                TempData["SuccessMessage"] = "Başarıyla kayıt oldunuz. Giriş yapabilirsiniz.";
            return RedirectToAction("KayitOl","Giris");

        }

        public IActionResult AdminGirisi()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminGirisi(AdminGirisModel model)
        {
            if (ModelState.IsValid)
            {

                if (AdminDogrulama(model.KullaniciAdi, model.Sifre))
                {
                    return RedirectToAction("Index", "YonetimPaneli", new { area = "Admin" });
                }
                else
                {
                    TempData["ErrorMessage"] = "Kullanıcı adı veya şifre yanlış";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        private bool AdminDogrulama(string KullaniciAdi, string password)
        {
            // Kullanıcı adı ve şifreyi kontrol et, örneğin bir veritabanından kontrol edilebilir
            // Bu örnekte basit bir kontrol yapısı kullanılmıştır, gerçek projelerde daha güvenli bir yöntem kullanılmalıdır.
            return (KullaniciAdi == "admin" && password == "admin123");
        }

        public ActionResult Cikis()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Anasayfa");
        }
    }
}
