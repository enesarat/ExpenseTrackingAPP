using AutoMapper;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.PaymentType;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.API.Controllers
{
    public class PaymentTypeController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IPaymentTypeService _service;

        public PaymentTypeController(IPaymentTypeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var paymentType = await _service.GetByIdAsync(id);
            var paymentTypeAsDto = _mapper.Map<PaymentTypeDto>(paymentType);

            return CustomActionResult(CustomResponse<PaymentTypeDto>.Success(200, paymentTypeAsDto));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var paymentTypes = await _service.GetAllAsync();
            var paymentTypesAsDto = _mapper.Map<List<PaymentTypeDto>>(paymentTypes.ToList());

            return CustomActionResult(CustomResponse<List<PaymentTypeDto>>.Success(200, paymentTypesAsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Post(PaymentTypeCreateDto paymentTypeDto)
        {
            var paymentType = _mapper.Map<PaymentType>(paymentTypeDto);
            await _service.AddAsync(paymentType);

            return CustomActionResult(CustomResponse<PaymentTypeCreateDto>.Success(201, paymentTypeDto));
        }

        [HttpPut]
        public async Task<IActionResult> Put(PaymentTypeUpdateDto paymentTypeDto)
        {
            var paymentType = _mapper.Map<PaymentType>(paymentTypeDto);
            await _service.UpdateAsync(paymentType);

            return CustomActionResult(CustomResponse<NoContentResponse>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var paymentType = await _service.GetByIdAsync(id);
            if (paymentType == null)
            {
                return CustomActionResult(CustomResponse<NoContentResponse>.Fail(404, $"{typeof(PaymentType).Name} ({id}) not found. Delete operation is not successfull. "));
            }
            await _service.DeleteAsync(id);

            return CustomActionResult(CustomResponse<NoContentResponse>.Success(204));
        }
    }
}
