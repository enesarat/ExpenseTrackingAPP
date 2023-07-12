using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Repositories;
using ExpenseTracking.Core.Services;
using ExpenseTracking.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Service.Services
{
    public class ExpenseService : GenericService<Expense>, IExpenseService
    {
        public ExpenseService(IGenericRepository<Expense> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
