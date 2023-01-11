using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class SolucionesContext : DbContext
    {
        public SolucionesContext(DbContextOptions<SolucionesContext> options)
            : base(options)
        {
            
        }
        public DbSet<Trabajo> Trabajos { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<TrabajoImage> TrabajosImages { get; set; }
        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trabajo>().ToTable("Trabajo");
            modelBuilder.Entity<Image>().ToTable("Image");
            modelBuilder.Entity<TrabajoImage>().ToTable("TrabajoImage");
            modelBuilder.Entity<Catalogo>().ToTable("Catalogo");
            modelBuilder.Entity<User>().ToTable("User");
        }

    }
}