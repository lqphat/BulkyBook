using System;
using System.Collections.Generic;
using System.Text;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryModel>()
                .HasData(
                new CategoryModel { Id = 1, Name = "Anime"},
                new CategoryModel { Id = 2, Name = "Lich Su" },
                new CategoryModel { Id = 3, Name = "Kiem Hiep" }
                );
            modelBuilder.Entity<CoverTypeModel>()
                .HasData(
                new CoverTypeModel { Id = 1, Name = "Bia Cung" },
                new CoverTypeModel { Id = 2, Name = "Bia Mem" }
                );
            base.OnModelCreating(modelBuilder);
        }

        public  DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CoverTypeModel> CoverTypes { get; set; }
        public DbSet<ProductModel> Products { get; set; }

    }
}