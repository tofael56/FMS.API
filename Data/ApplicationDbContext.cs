using FMS.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FMS.API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel()
                {
                    Id = 1,
                    Name = "Papaya Green Queen",
                    Code = "001",
                    Barcode = "papaya-001",
                    CreateDate = DateTime.Now,
                },
                 new ProductModel()
                 {
                     Id = 2,
                     Name = "Papaya Shahi",
                     Code = "002",
                     Barcode = "papaya-shani-002",
                     CreateDate = DateTime.Now,
                 },
                  new ProductModel()
                  {
                      Id = 3,
                      Name = "Papaya Babu",
                      Code = "003",
                      Barcode = "papaya-babu-003",
                      CreateDate = DateTime.Now,
                  },
                   new ProductModel()
                   {
                       Id = 4,
                       Name = "Papaya Top Lady",
                       Code = "004",
                       Barcode = "papaya-004",
                       CreateDate = DateTime.Now,
                   }
                );
        }
    }
}
