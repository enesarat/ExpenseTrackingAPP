using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Expense;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Services
{
    public interface IExpenseService : IGenericService<Expense, ExpenseDto>
    {
        public Task<CustomResponse<NoContentResponse>> AddAsync(ExpenseCreateDto expenseCreateDto);
        public Task<CustomResponse<NoContentResponse>> UpdateAsync(ExpenseUpdateDto expenseUpdateDto);
        public Task<CustomResponse<IEnumerable<ExpenseDto>>> GetExpensesWithDetailsAsync();
        public Task<CustomResponse<ExpenseDto>> GetExpenseWithDetailsAsync(int id);
    }
}
