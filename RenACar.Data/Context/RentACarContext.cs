using RentACar.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Data.Context
{
    public class RentACarContext : DbContext
    {

        public RentACarContext(DbContextOptions<RentACarContext> options) : base(options) 
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());

            base.OnModelCreating(modelBuilder);

        }



        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();


    }
}
