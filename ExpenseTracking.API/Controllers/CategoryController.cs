using AutoMapper;
using ExpenseTracking.API.Filters;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.DTOs.Concrete.User;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.API.Controllers
{
    public class CategoryController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service, IMapper mapper)
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
        public async Task<IActionResult> Post(CategoryCreateDto categoryDto)
        {
            return CustomActionResult(await _service.AddAsync(categoryDto));
        }

        [HttpPut]
        [ServiceFilter(typeof(CreateDateSafetyFilter<Category, CategoryUpdateDto>))]
        [ServiceFilter(typeof(CreatedBySafetyFilter<Category, CategoryUpdateDto>))]
        public async Task<IActionResult> Put(CategoryUpdateDto categoryDto)
        {
            return CustomActionResult(await _service.UpdateAsync(categoryDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CustomActionResult(await _service.DeleteAsync(id));
        }
    }
}
