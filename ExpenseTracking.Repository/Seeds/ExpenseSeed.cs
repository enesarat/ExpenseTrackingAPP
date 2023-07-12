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
    public class ExpenseSeed : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasData(
               new Expense { Id = 1, Name = "Market Alışverişi", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true,CategoryId=1,Cost=560,Description="Hazırlanan listeye göre aylık market alışverişi yapıldı.",PaymentTypeId=1,UserId=1,TransactionDate=DateTime.Now },
               new Expense { Id = 2, Name = "Deterjan ve Peçete", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true, CategoryId = 2, Cost = 300, Description = "Bulaşık deterjanı, peçete ve tuvalet kağıdı alındı.", PaymentTypeId = 1, UserId = 1, TransactionDate = DateTime.Now },
               new Expense { Id = 3, Name = "Kablosuz Süpürge", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true, CategoryId = 3, Cost = 3400, Description = "X markanın şarjlı elektrikli süpürgesinden alındı.", PaymentTypeId = 2, UserId = 2, TransactionDate = DateTime.Now },
               new Expense { Id = 4, Name = "Duman Konseri", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true, CategoryId = 4, Cost = 850, Description = "Duman Grubu'nun 22 Temmuz'daki Harbiye açık hava konserine bilet alındı.", PaymentTypeId = 2, UserId = 2, TransactionDate = DateTime.Now },
               new Expense { Id = 5, Name = "KBB Doktor Kontrolü", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true, CategoryId = 5, Cost = 200, Description = "KBB Doktor kontrolüne gidildi ve ilaç alındı.", PaymentTypeId = 2, UserId = 1, TransactionDate = DateTime.Now },
               new Expense { Id = 6, Name = "Telefon Kılıfı", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true, CategoryId = 6, Cost = 250, Description = "Telefonun yırtılan kılıfının yerine yenisi alındı.", PaymentTypeId = 1, UserId = 2, TransactionDate = DateTime.Now },
               new Expense { Id = 7, Name = "Ayakkabı", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true, CategoryId = 7, Cost = 1190, Description = "Eskiyen ayakkabı yerine Converse ayakkabı alındı.", PaymentTypeId = 1, UserId = 2, TransactionDate = DateTime.Now },
               new Expense { Id = 8, Name = "Pizza ve İçecek", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true, CategoryId = 1, Cost = 850, Description = "Akşam iş dönüşü dışarıda arkadaşlarla pizza ziyafeti.", PaymentTypeId = 1, UserId = 1, TransactionDate = DateTime.Now },
               new Expense { Id = 9, Name = "Kuaför ve Şampuan", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true, CategoryId = 2, Cost = 300, Description = "Kuaförde traş olunup, özel şampuan alındı.", PaymentTypeId = 1, UserId = 1, TransactionDate = DateTime.Now },
               new Expense { Id = 10, Name = "Powerbank", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true, CategoryId = 3, Cost = 430, Description = "Telefon ve tablet için powerbank alındı.", PaymentTypeId = 1, UserId = 2, TransactionDate = DateTime.Now },
               new Expense { Id = 11, Name = "Sinema", CreatedDate = DateTime.Now, CreatedBy = "SYSTEM", IsActive = true, CategoryId = 4, Cost = 85, Description = "Eskiyen ayakkabı yerine Converse ayakkabı alındı.", PaymentTypeId = 1, UserId = 2, TransactionDate = DateTime.Now }
               );
        }
    }
}
