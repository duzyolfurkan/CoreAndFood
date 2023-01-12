using CoreAndFood.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreAndFood.Data
{
    public class Context : DbContext
    {
        //Server bağlantısı
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-LUPKI70;Database=DbCoreFoodV2;Trusted_Connection=True;");
        }

        //Tabloların Dontext'e tanımlanması
        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
