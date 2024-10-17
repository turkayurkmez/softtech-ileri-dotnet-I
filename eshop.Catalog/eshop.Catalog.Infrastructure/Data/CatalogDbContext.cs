using eshop.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Catalog.Infrastructure.Data
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<Product>  Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options):base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Kırtasiye" },
                new Category { Id = 2, Name = "Mobilya" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id=1, Name="Beyaz Tahta", Description="250x250", ImageUrl="sample.png", Price=1, CategoryId=1},
                new Product { Id = 2, Name = "Orta sehpa", Description = "Fiskos :)", ImageUrl = "sample.png", Price = 1, CategoryId = 2 },
                new Product { Id = 3, Name = "Kalem seti", Description = "Faber Castel", ImageUrl = "sample.png", Price = 1, CategoryId = 1 }
              );


        }
    }
}
