using Microsoft.EntityFrameworkCore;

namespace deploymentWithDocker.Data
{
    public class SampleDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public SampleDbContext(DbContextOptions<SampleDbContext> options):base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "ilk müşteri", Address = "a sokak", City = "İstanbul" },
                new Customer { Id = 2, Name = "ikinci müşteri", Address = "b sokak", City = "Ankara" },
                new Customer { Id = 3, Name = "üçüncü müşteri", Address = "c sokak", City = "Eskişehir" }



            );
        }
    }
}
