using AutoMapper;
using ExpenseTracking.Core.DTOs.Concrete.Account;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Repositories;
using ExpenseTracking.Core.Services;
using ExpenseTracking.Core.UnitOfWorks;
using ExpenseTracking.Repository.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Service.Services
{
    public class CategoryService : GenericService<Category,CategoryDto>, ICategoryService
    {   
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IUserRepository userRepository, IHttpContextAccessor contextAccessor,IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<CustomResponse<NoContentResponse>> AddAsync(CategoryCreateDto categoryCreateDto)
        {
            if (await CategoryVerifier(categoryCreateDto.Name))
            {
                return CustomResponse<NoContentResponse>.Fail(StatusCodes.Status400BadRequest, "This category name is registered in the system. Please specify another category name.");
            }
            var item = _mapper.Map<Category>(categoryCreateDto);
            item.CreatedDate = DateTime.Now;
            using (var currentAccount = GetCurrentAccount())
            {
                item.CreatedBy = currentAccount.Result.Email;
            }
            await _categoryRepository.AddAsync(item);
            await _unitOfWork.CommitAsync();

            return CustomResponse<NoContentResponse>.Success(StatusCodes.Status204NoContent);
        }
        public async Task<bool> CategoryVerifier(string name)
        {
            if (await _categoryRepository.AnyAsync(x => x.Name == name))
            {
                return true;
            }
            return false;
        }
        public async Task<CustomResponse<NoContentResponse>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            if (await _categoryRepository.AnyAsync(x => x.Id == categoryUpdateDto.Id && x.IsActive == true))
            {
                var entity = _mapper.Map<Category>(categoryUpdateDto);

                entity.UpdatedDate = DateTime.Now;
                _categoryRepository.Update(entity);
                await _unitOfWork.CommitAsync();
                return CustomResponse<NoContentResponse>.Success(StatusCodes.Status204NoContent);
            }
            return CustomResponse<NoContentResponse>.Fail(StatusCodes.Status404NotFound, $" {typeof(Category).Name} ({categoryUpdateDto.Id}) not found. Updete operation is not successfull. ");
        }

        public async Task<ActiveAccountDto> GetCurrentAccount()
        {
            var identity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userClaims = identity.Claims;
            var accountEmail = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
            var user = _userRepository.Where(x => x.Email == accountEmail).FirstOrDefault();

            if (user != null && user.RefreshToken != null)
            {
                ActiveAccountDto currentaccount = new ActiveAccountDto
                {
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value

                };
                return currentaccount;
            }
            else
                throw new InvalidOperationException("Could not access active user information.");
        }
    }
}
