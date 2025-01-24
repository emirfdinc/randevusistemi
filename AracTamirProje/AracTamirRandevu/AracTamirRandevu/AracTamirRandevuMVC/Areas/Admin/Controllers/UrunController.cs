using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AracTamirRandevuMVC.Context;

namespace AracTamirRandevuMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UrunController : Controller
    {
        private readonly AracTamirRandevuDbContext _context;

        public UrunController(AracTamirRandevuDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var urunler = await _context.Urunler.ToListAsync();
            return View(urunler);
        }

        [HttpPost]
        public async Task<IActionResult> StokGuncelle(int urunID, int yeniStok)
        {
            var urun = await _context.Urunler.FindAsync(urunID);
            if (urun == null)
            {
                return NotFound();
            }

            urun.Stok = yeniStok;
            _context.Update(urun);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
