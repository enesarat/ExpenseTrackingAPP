using AutoMapper;
using ExpenseTracking.API.Filters;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Expense;
using ExpenseTracking.Core.DTOs.Concrete.Response;
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
            var expense = await _service.GetByIdAsync(id);
            var expenseAsDto = _mapper.Map<ExpenseDto>(expense);

            return CustomActionResult(CustomResponse<ExpenseDto>.Success(200, expenseAsDto));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var expenses = await _service.GetAllAsync();
            var expensesAsDto = _mapper.Map<List<ExpenseDto>>(expenses.ToList());

            return CustomActionResult(CustomResponse<List<ExpenseDto>>.Success(200, expensesAsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExpenseCreateDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);
            await _service.AddAsync(expense);

            return CustomActionResult(CustomResponse<ExpenseCreateDto>.Success(201, expenseDto));
        }

        [HttpPut]
        [ServiceFilter(typeof(UpdateUserIdSafetyFilter<Expense, ExpenseUpdateDto>))]
        public async Task<IActionResult> Put(ExpenseUpdateDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);
            await _service.UpdateAsync(expense);

            return CustomActionResult(CustomResponse<NoContentResponse>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _service.GetByIdAsync(id);
            if (expense == null)
            {
                return CustomActionResult(CustomResponse<NoContentResponse>.Fail(404, $"{typeof(Expense).Name} ({id}) not found. Delete operation is not successfull. "));
            }
            await _service.DeleteAsync(id);

            return CustomActionResult(CustomResponse<NoContentResponse>.Success(204));
        }
    }
}
