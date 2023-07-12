using AutoMapper;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Response;
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
            var catgory = await _service.GetByIdAsync(id);
            var catgoryAsDto = _mapper.Map<CategoryDto>(catgory);

            return CustomActionResult(CustomResponse<CategoryDto>.Success(200, catgoryAsDto));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var catgories = await _service.GetAllAsync();
            var catgoriesAsDto = _mapper.Map<List<CategoryDto>>(catgories.ToList());

            return CustomActionResult(CustomResponse<List<CategoryDto>>.Success(200, catgoriesAsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _service.AddAsync(category);

            return CustomActionResult(CustomResponse<CategoryCreateDto>.Success(201, categoryDto));
        }

        [HttpPut]
        public async Task<IActionResult> Put(CategoryUpdateDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _service.UpdateAsync(category);

            return CustomActionResult(CustomResponse<NoContentResponse>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null)
            {
                return CustomActionResult(CustomResponse<NoContentResponse>.Fail(404, $"{typeof(Category).Name} ({id}) not found. Delete operation is not successfull. "));
            }
            await _service.DeleteAsync(id);

            return CustomActionResult(CustomResponse<NoContentResponse>.Success(204));
        }
    }
}
