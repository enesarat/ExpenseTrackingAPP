﻿using ExpenseTracking.Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Repositories
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        Task<IEnumerable<Expense>> GetExpensesWithDetails();
        Task<Expense> GetExpenseWithDetails(int id);

        Task<IEnumerable<Expense>> GetExpensesForUser(int userId);
    }
}
