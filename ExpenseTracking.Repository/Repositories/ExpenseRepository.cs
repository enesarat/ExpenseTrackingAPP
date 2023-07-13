using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Repositories;
using ExpenseTracking.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Repository.Repositories
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Expense>> GetExpensesForUser(int userId)
        {
            return _context.Expenses.Where(x => x.User.Id == userId).AsEnumerable();
        }

        public async Task<IEnumerable<Expense>> GetExpensesWithDetails()
        {
            //Eager Loading
            return _context.Expenses.AsNoTracking().Include(x => x.Category).Include(x => x.User).Include(x => x.PaymentType).AsEnumerable();
        }


        public Task<Expense> GetExpenseWithDetails(int id)
        {
            //Eager Loading
            return _context.Expenses.Include(x => x.Category).Include(x => x.User).Include(x => x.PaymentType).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
