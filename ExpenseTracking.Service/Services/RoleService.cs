using AutoMapper;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.DTOs.Concrete.Role;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Repositories;
using ExpenseTracking.Core.Services;
using ExpenseTracking.Core.UnitOfWorks;
using ExpenseTracking.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Service.Services
{
    public class RoleService : GenericService<Role,RoleDto>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        public RoleService(IGenericRepository<Role> repository, IUnitOfWork unitOfWork, IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<CustomResponse<NoContentResponse>> AddAsync(RoleCreateDto roleCreateDto)
        {
            if (await RoleVerifier(roleCreateDto.Name))
            {
                return CustomResponse<NoContentResponse>.Fail(StatusCodes.Status400BadRequest, "This category name is registered in the system. Please specify another category name.");
            }
            var item = _mapper.Map<Role>(roleCreateDto);
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = "SYSTEM";
            await _roleRepository.AddAsync(item);
            await _unitOfWork.CommitAsync();

            return CustomResponse<NoContentResponse>.Success(StatusCodes.Status204NoContent);
        }
        public async Task<bool> RoleVerifier(string name)
        {
            if (await _roleRepository.AnyAsync(x => x.Name == name))
            {
                return true;
            }
            return false;
        }
        public async Task<CustomResponse<NoContentResponse>> UpdateAsync(RoleUpdateDto roleUpdateDto)
        {
            if (await _roleRepository.AnyAsync(x => x.Id == roleUpdateDto.Id && x.IsActive == true))
            {
                var entity = _mapper.Map<Role>(roleUpdateDto);

                entity.UpdatedDate = DateTime.Now;
                _roleRepository.Update(entity);
                await _unitOfWork.CommitAsync();
                return CustomResponse<NoContentResponse>.Success(StatusCodes.Status204NoContent);
            }
            return CustomResponse<NoContentResponse>.Fail(StatusCodes.Status404NotFound, $" {typeof(Category).Name} ({roleUpdateDto.Id}) not found. Updete operation is not successfull. ");
        }
    }
}
