﻿using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Repositories;
using ExpenseTracking.Repository.Contexts;
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
    }
}
