using AutoMapper;
using ExpenseTracking.Core.DTOs.Concrete.Account;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.PaymentType;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Repositories;
using ExpenseTracking.Core.Services;
using ExpenseTracking.Core.UnitOfWorks;
using ExpenseTracking.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Service.Services
{
    public class PaymentTypeService : GenericService<PaymentType, PaymentTypeDto>, IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public PaymentTypeService(IGenericRepository<PaymentType> repository, IUnitOfWork unitOfWork, IPaymentTypeRepository paymentTypeRepository, IUserRepository userRepository, IMapper mapper, IHttpContextAccessor contextAccessor) : base(repository, unitOfWork, mapper)
        {
            _paymentTypeRepository = paymentTypeRepository;
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<CustomResponse<NoContentResponse>> AddAsync(PaymentTypeCreateDto paymentTypeCreateDto)
        {
            if (await PaymentTypeVerifier(paymentTypeCreateDto.Name))
            {
                return CustomResponse<NoContentResponse>.Fail(StatusCodes.Status400BadRequest, "This category name is registered in the system. Please specify another category name.");
            }
            var item = _mapper.Map<PaymentType>(paymentTypeCreateDto);
            item.CreatedDate = DateTime.Now;
            using (var currentAccount = GetCurrentAccount())
            {
                item.CreatedBy = currentAccount.Result.Email;
            }
            await _paymentTypeRepository.AddAsync(item);
            await _unitOfWork.CommitAsync();

            return CustomResponse<NoContentResponse>.Success(StatusCodes.Status204NoContent);
        }
        public async Task<bool> PaymentTypeVerifier(string name)
        {
            if (await _paymentTypeRepository.AnyAsync(x => x.Name == name))
            {
                return true;
            }
            return false;
        }

        public async Task<CustomResponse<NoContentResponse>> UpdateAsync(PaymentTypeUpdateDto paymentTypeUpdateDto)
        {
            if (await _paymentTypeRepository.AnyAsync(x => x.Id == paymentTypeUpdateDto.Id && x.IsActive == true))
            {
                var entity = _mapper.Map<PaymentType>(paymentTypeUpdateDto);

                entity.UpdatedDate = DateTime.Now;
                _paymentTypeRepository.Update(entity);
                await _unitOfWork.CommitAsync();
                return CustomResponse<NoContentResponse>.Success(StatusCodes.Status204NoContent);
            }
            return CustomResponse<NoContentResponse>.Fail(StatusCodes.Status404NotFound, $" {typeof(Category).Name} ({paymentTypeUpdateDto.Id}) not found. Updete operation is not successfull. ");
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
