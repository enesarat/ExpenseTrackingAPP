using ExpenseTracking.API.Filters;
using ExpenseTracking.Core.DTOs.Concrete.Account;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.DTOs.Concrete.User;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Models.Token;
using ExpenseTracking.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.API.Controllers
{
    public class AccountController : CustomBaseController
    {
        private readonly IAccountService _service;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountController(IAccountService service, IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _service = service;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }
        /// <summary>
        /// This endpoint allows to login token for a session according to given account informations.
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("connect/token")]
        public async Task<IActionResult> Login([FromBody] TokenRequest userLogin)
        {
            return Ok(await _service.Login(userLogin));
        }

        /// <summary>
        /// This endpoind allows to logout from active session.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            return CustomActionResult(await _service.Logout(_contextAccessor.HttpContext));
        }
        /// <summary>
        /// This endpoind allows to refreshing session life over token of active account. Gives a new auth token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromQuery] string token)
        {
            return Ok(await _service.RefreshToken(token));
        }
        /// <summary>
        /// This endpoind allows informatins of active user.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCurrentAccount()
        {
            return CustomActionResult(CustomResponse<ActiveAccountDto>.Success(StatusCodes.Status200OK, await _service.GetCurrentAccount()));
        }
        /// <summary>
        /// This endpoind allows the user informations according to given id info.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CustomActionResult(await _userService.GetUserWithRoleAsync(id));
        }
        /// <summary>
        /// This endpoind allows the all user informations which are active.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CustomActionResult(await _userService.GetUsersWithRoleAsync());
        }

        /// <summary>
        /// This endpoint allows to create a user that has standart profile type.
        /// </summary>
        /// <param name="personnelRoleDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateStandartUser(UserCreateDto personnelRoleDto)
        {
            return CustomActionResult(await _service.AddStandartUserAsync(personnelRoleDto));
        }

        /// <summary>
        /// This endpoint allows to create a user that has admin profile type.
        /// </summary>
        /// <param name="adminRoleDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAdmin(UserCreateDto adminRoleDto)
        {
            return CustomActionResult(await _service.AddAdminAsync(adminRoleDto));
        }

        /// <summary>
        /// This endpoint allows to update the user with admin privileges. Role assignment can change.
        /// </summary>
        /// <param name="userUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsAdmin")]
        [ServiceFilter(typeof(CreateDateSafetyFilter<User, UserUpdateAsAdminDto>))]
        [ServiceFilter(typeof(CreatedBySafetyFilter<User, UserUpdateAsAdminDto>))]
        public async Task<IActionResult> UpdateAsAdmin([FromBody] UserUpdateAsAdminDto userUpdateDto)
        {
            return CustomActionResult(await _service.UpdateAsAdminAsync(userUpdateDto));
        }

        /// <summary>
        /// This endpoint allows to update user information.
        /// </summary>
        /// <param name="userUpdateDto"></param>
        /// <returns></returns>
        [HttpPut]
        [ServiceFilter(typeof(CreateDateSafetyFilter<User, UserUpdateDto>))]
        [ServiceFilter(typeof(CreatedBySafetyFilter<User, UserUpdateDto>))]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto userUpdateDto)
        {
            return CustomActionResult(await _service.UpdateAsync(userUpdateDto));
        }

        /// <summary>
        /// This endpoint allows to delete the user. (Disables)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CustomActionResult(await _service.DeleteAsync(id));
        }
    }
}
