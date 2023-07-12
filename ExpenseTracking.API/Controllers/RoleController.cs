using AutoMapper;
using ExpenseTracking.API.Filters;
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
            var role = await _service.GetByIdAsync(id);
            var roleAsDto = _mapper.Map<RoleDto>(role);

            return CustomActionResult(CustomResponse<RoleDto>.Success(200, roleAsDto));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _service.GetAllAsync();
            var rolesAsDto = _mapper.Map<List<RoleDto>>(roles.ToList());

            return CustomActionResult(CustomResponse<List<RoleDto>>.Success(200, rolesAsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Post(RoleCreateDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            await _service.AddAsync(role);

            return CustomActionResult(CustomResponse<RoleCreateDto>.Success(201, roleDto));
        }

        [HttpPut]
        public async Task<IActionResult> Put(RoleUpdateDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            await _service.UpdateAsync(role);

            return CustomActionResult(CustomResponse<NoContentResponse>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _service.GetByIdAsync(id);
            if (role == null)
            {
                return CustomActionResult(CustomResponse<NoContentResponse>.Fail(404, $"{typeof(Role).Name} ({id}) not found. Delete operation is not successfull. "));
            }
            await _service.DeleteAsync(id);

            return CustomActionResult(CustomResponse<NoContentResponse>.Success(204));
        }
    }
}
