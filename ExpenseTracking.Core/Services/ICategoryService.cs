using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Services
{
    public interface ICategoryService : IGenericService<Category,CategoryDto>
    {
        public Task<CustomResponse<NoContentResponse>> AddAsync(CategoryCreateDto categoryCreateDto);
        public Task<CustomResponse<NoContentResponse>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
    }
}
