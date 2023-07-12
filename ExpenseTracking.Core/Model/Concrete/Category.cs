﻿using ExpenseTracking.Core.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Model.Concrete
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
