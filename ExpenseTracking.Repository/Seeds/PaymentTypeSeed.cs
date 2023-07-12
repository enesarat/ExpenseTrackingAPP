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
    public class PaymentTypeSeed : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasData(
                new PaymentType { Id = 1, Name = "Nakit", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true },
                new PaymentType { Id = 2, Name = "Kredi", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true }
                );
        }
    }
}
