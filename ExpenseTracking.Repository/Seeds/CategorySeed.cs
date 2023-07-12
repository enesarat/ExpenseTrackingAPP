using ExpenseTracking.Core.Models.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Repository.Seeds
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Beslenme/Gıda", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true },
                new Category { Id = 2, Name = "Temizlik", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true },
                new Category { Id = 3, Name = "Teknolojik Alışveriş", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true },
                new Category { Id = 4, Name = "Eğlence", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true },
                new Category { Id = 5, Name = "Sağlık", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true },
                new Category { Id = 6, Name = "Acil İhtiyaç", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true },
                new Category { Id = 7, Name = "Giyim", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true }
                );
        }
    }
}
