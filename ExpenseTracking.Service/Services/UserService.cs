using AutoMapper;
using ExpenseTracking.Core.DTOs.Concrete.Account;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.DTOs.Concrete.User;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Repositories;
using ExpenseTracking.Core.Services;
using ExpenseTracking.Core.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Service.Services
{
    public class UserService : GenericService<User, UserDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, IHttpContextAccessor contextAccessor) : base(repository, unitOfWork, mapper)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<CustomResponse<NoContentResponse>> AddAsync(UserCreateDto userCreateDto)
        {
            var item = _mapper.Map<User>(userCreateDto);
            item.CreatedDate = DateTime.Now;
            using (var currentAccount = GetCurrentAccount())
            {
                item.CreatedBy = currentAccount.Result.Email;
            }
            item.RoleId = 2;
            await _userRepository.AddAsync(item);
            await _unitOfWork.CommitAsync();

            return CustomResponse<NoContentResponse>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponse<IEnumerable<UserDto>>> GetUsersWithRoleAsync()
        {
            var entities = await _userRepository.GetUsersWithRole();
            var entitiesQueryable = entities.ToList().AsQueryable();
            var activeEntities = entitiesQueryable.Where(x => x.IsActive == true);

            var entitiesAsDto = _mapper.Map<IEnumerable<UserDto>>(activeEntities);
            return CustomResponse<IEnumerable<UserDto>>.Success(StatusCodes.Status200OK, entitiesAsDto);
        }

        public async Task<CustomResponse<UserDto>> GetUserWithRoleAsync(int id)
        {
            var entity = await _userRepository.GetUserWithRole(id);
            if (entity != null && entity.IsActive != false)
            {
                var entityAsDto = _mapper.Map<UserDto>(entity);

                return CustomResponse<UserDto>.Success(StatusCodes.Status200OK, entityAsDto);
            }
            return CustomResponse<UserDto>.Fail(StatusCodes.Status404NotFound, $" {typeof(User).Name} ({id}) not found. Retrieve operation is not successfull. ");
        }

        public async Task<CustomResponse<NoContentResponse>> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            if (await _userRepository.AnyAsync(x => x.Id == userUpdateDto.Id && x.IsActive == true))
            {
                var entity = _mapper.Map<User>(userUpdateDto);

                entity.UpdatedDate = DateTime.Now;
                entity.RoleId = 2;
                _userRepository.Update(entity);
                await _unitOfWork.CommitAsync();
                return CustomResponse<NoContentResponse>.Success(StatusCodes.Status204NoContent);
            }
            return CustomResponse<NoContentResponse>.Fail(StatusCodes.Status404NotFound, $" {typeof(User).Name} ({userUpdateDto.Id}) not found. Updete operation is not successfull. ");
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
