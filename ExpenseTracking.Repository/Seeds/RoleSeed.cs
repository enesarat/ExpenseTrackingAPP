using ExpenseTracking.Core.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Repository.Seeds
{
    public class RoleSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { Id = 1, Name = "Admin", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true },
                new Role { Id = 2, Name = "Standart", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true }
                );
        }
    }
}
