using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AracTamirRandevuMVC.Context;
using AracTamirRandevuMVC.Models;

namespace AracTamirRandevuMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RandevuController : Controller
    {
        private readonly AracTamirRandevuDbContext _context;

        public RandevuController(AracTamirRandevuDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            var Randevular = _context.Randevular
                .Where(a => a.OnayDurumu == "Onay Bekliyor") // Sadece "Onay Bekliyor" durumundaki randevuları al
                .Include(a => a.Araba)
                .Include(a => a.Araba.Kullanici)
                .Select(a => new RandevuViewModel
                {
                    RandevuID = a.randevuID,
                    RandevuTarihi = a.RandevuTarihi,
                    Aciklama = a.Aciklama,
                    OnayDurumu = a.OnayDurumu,
                    OlusturulmaTarihi = a.OlusturulmaTarihi,
                    KullaniciAdi = a.Araba != null && a.Araba.Kullanici != null ? a.Araba.Kullanici.KullaniciAdi : ""
                })
                .ToList();

            return View(Randevular);
        }

        public async Task<IActionResult> Onayla(int randevuID)
        {
            var Randevu = await _context.Randevular.FindAsync(randevuID);
            if (Randevu == null)
            {
                return NotFound();
            }

            Randevu.OnayDurumu = "Onaylandı"; // Statusu güncelle
            await _context.SaveChangesAsync();

            var araba = await _context.Arabalar.FindAsync(Randevu.ArabaID);
            if (araba != null && araba.KullaniciID != null)
            {
                var bildirim = new Bildirim
                {
                    KullaniciID = (int)araba.KullaniciID,
                    RandevuID = randevuID,
                    BildirimMetni = "Randevunuz başarıyla onaylanmıştır.",
                    OlusturulmaTarihi = DateTime.Now,
                    OkunduBilgisi = false
                };
                _context.Bildirimler.Add(bildirim);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reddet(int randevuID)
        {
            var randevu = await _context.Randevular.FindAsync(randevuID);
            if (randevu == null)
            {
                return NotFound();
            }

            randevu.OnayDurumu = "Reddedildi"; 
            await _context.SaveChangesAsync();

            // ArabaID'den Araba nesnesini bul ve KullaniciID'yi al
            var araba = await _context.Arabalar.FindAsync(randevu.ArabaID);
            if (araba != null && araba.KullaniciID != null)
            {
                var bildirim = new Bildirim
                {
                    KullaniciID = (int)araba.KullaniciID,
                    RandevuID = randevuID,
                    BildirimMetni = "Randevu işleminiz oluşturulamadı.",
                    OlusturulmaTarihi = DateTime.Now,
                    OkunduBilgisi = false
                };
                _context.Bildirimler.Add(bildirim);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
