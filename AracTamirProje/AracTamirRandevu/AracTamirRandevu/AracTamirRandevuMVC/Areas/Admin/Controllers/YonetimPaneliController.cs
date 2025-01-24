using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using AracTamirRandevuMVC.Context;

namespace AracTamirRandevuMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class YonetimPaneliController : Controller
    {
        private readonly AracTamirRandevuDbContext _context;
        public YonetimPaneliController(AracTamirRandevuDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var arabaKullanicilari = from araba in _context.Arabalar
                           join kullanici in _context.Kullanicilar on araba.KullaniciID equals kullanici.KullaniciID
                           select new
                           {
                               araba.Marka,
                               araba.Model,
                               araba.Yil,
                               araba.Plaka,
                               kullanici.Ad,
                               kullanici.Soyad,
                               kullanici.KullaniciAdi
                           };

            var expandoList = new List<ExpandoObject>();
            foreach (var item in arabaKullanicilari)
            {
                dynamic expando = new ExpandoObject();
                expando.Marka = item.Marka;
                expando.Model = item.Model;
                expando.Yil = item.Yil;
                expando.Plaka = item.Plaka;
                expando.Ad = item.Ad;
                expando.Soyad = item.Soyad;
                expando.KullaniciAdi = item.KullaniciAdi;
                expandoList.Add(expando);
            }

            return View(expandoList);
        }

    }
}
