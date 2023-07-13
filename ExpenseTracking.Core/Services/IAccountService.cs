using ExpenseTracking.Core.DTOs.Concrete.Account;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.DTOs.Concrete.Token;
using ExpenseTracking.Core.DTOs.Concrete.User;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Models.Token;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Services
{
    public interface IAccountService : IGenericService<User, UserDto>
    {
        Task<CustomResponse<NoContentResponse>> UpdateAsync(UserUpdateDto dto);
        Task<CustomResponse<NoContentResponse>> UpdateAsAdminAsync(UserUpdateAsAdminDto userUpdateDto);
        Task<CustomResponse<NoContentResponse>> AddStandartUserAsync(UserCreateDto dto);
        Task<CustomResponse<NoContentResponse>> AddAdminAsync(UserCreateDto dto);
        Task<CustomResponse<UserDto>> GetByIdAsync(int id);
        UserDto Authenticate(TokenRequest userLogin);
        TokenDto GenerateToken(UserDto user);
        Task<TokenDto> Login(TokenRequest userLogin);
        Task<CustomResponse<NoContentResponse>> Logout(HttpContext context);

        Task<TokenDto> RefreshToken(string tokenStr);
        Task<ActiveAccountDto> GetCurrentAccount();
    }
}
