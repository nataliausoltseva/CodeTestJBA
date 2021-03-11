using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodProducts.Models
{
    public class FoodProductsContext : DbContext
    {
        public FoodProductsContext(DbContextOptions<FoodProductsContext> options) : base(options) { }
        public DbSet<MailingList> MailingList {get;set;}
        public DbSet<CurrentProducts> CurrentProducts { get; set; }
        public DbSet<NewProducts> NewProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MailingList>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<CurrentProducts>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<NewProducts>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
