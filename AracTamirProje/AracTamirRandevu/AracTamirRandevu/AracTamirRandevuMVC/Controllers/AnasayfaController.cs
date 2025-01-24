using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AracTamirRandevuMVC.Context;
using AracTamirRandevuMVC.Models;

namespace AracTamirRandevuMVC.Controllers
{
    public class AnasayfaController : Controller
    {
        private readonly ILogger<AnasayfaController> _logger;
        private readonly AracTamirRandevuDbContext _context;

        public AnasayfaController(ILogger<AnasayfaController> logger, AracTamirRandevuDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var girisBilgisi = HttpContext.Session.GetString("GirisBilgisi");
            if (girisBilgisi == "true")
            {
                ViewBag.GirisBilgisi = true;
                ViewBag.KullaniciAdi = HttpContext.Session.GetString("KullaniciAdi");
            }
            else
            {
                ViewBag.GirisBilgisi = false;
            }

            return View();
        }

        public IActionResult Urunler()
        {
            ViewBag.girisBilgisi = HttpContext.Session.GetString("GirisBilgisi") == "true";
            var urunler=_context.Urunler.ToList();
            return View(urunler);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}