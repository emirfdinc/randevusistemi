using Microsoft.EntityFrameworkCore;
using AracTamirRandevuMVC.Models;

namespace AracTamirRandevuMVC.Context
{
    public class AracTamirRandevuDbContext : DbContext
    {
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<RandevuDurumDegisiklik> RandevuDurumDegisiklikler { get; set; }
        public DbSet<Araba> Arabalar { get; set; }
        public DbSet<Bildirim> Bildirimler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }

        public AracTamirRandevuDbContext(DbContextOptions<AracTamirRandevuDbContext> options) : base(options)
        {

        }

    }

   

}
