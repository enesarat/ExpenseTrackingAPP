using AutoMapper;
using ExpenseTracking.API.Filters;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Expense;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.DTOs.Concrete.Role;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.API.Controllers
{
    public class RoleController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _service;

        public RoleController(IRoleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return CustomActionResult(await _service.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CustomActionResult(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(RoleCreateDto roleDto)
        {
            return CustomActionResult(await _service.AddAsync(roleDto));
        }

        [HttpPut]
        public async Task<IActionResult> Put(RoleUpdateDto roleDto)
        {
            return CustomActionResult(await _service.UpdateAsync(roleDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CustomActionResult(await _service.DeleteAsync(id));
        }
    }
}
