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
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { Id = 1, Name = "Enes", Surname="Arat",Email="enes@gmail.com",Password= "$2a$11$Mnqi9RoGPlhOgFalgcahL.okqa2tnZgz3gtkTs7oeHFVmlSC507GS", RoleId=1, CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true },
                new User { Id = 2, Name = "Eren", Surname = "Arat", Email = "eren@gmail.com", Password = "$2a$11$Owh2PufGb1wWYTUt4bNUK.iqTrAMi5E09oRPFSIKVnuRawxm.flIO", RoleId = 2, CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true }
                );
        }
    }
}
