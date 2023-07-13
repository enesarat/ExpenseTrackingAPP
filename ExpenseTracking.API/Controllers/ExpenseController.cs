using AutoMapper;
using ExpenseTracking.API.Filters;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Expense;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.DTOs.Concrete.User;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracking.API.Controllers
{
    public class ExpenseController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IExpenseService _service;

        public ExpenseController(IExpenseService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return CustomActionResult(await _service.GetExpenseWithDetailsAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CustomActionResult(await _service.GetExpensesWithDetailsAsync());
        }
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetExpensesForUser(int id)
        {
            return CustomActionResult(await _service.GetExpensesForUser(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExpenseCreateDto expenseDto)
        {
            return CustomActionResult(await _service.AddAsync(expenseDto));
        }

        [HttpPut]
        [ServiceFilter(typeof(UpdateUserIdSafetyFilter<Expense, ExpenseUpdateDto>))]
        [ServiceFilter(typeof(CreateDateSafetyFilter<Expense, ExpenseUpdateDto>))]
        [ServiceFilter(typeof(CreatedBySafetyFilter<Expense, ExpenseUpdateDto>))]
        public async Task<IActionResult> Put(ExpenseUpdateDto expenseDto)
        {
            return CustomActionResult(await _service.UpdateAsync(expenseDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CustomActionResult(await _service.DeleteAsync(id));
        }
    }
}
