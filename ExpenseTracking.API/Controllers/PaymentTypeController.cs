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
            return CustomActionResult(await _service.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CustomActionResult(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(PaymentTypeCreateDto paymentTypeDto)
        {
            return CustomActionResult(await _service.AddAsync(paymentTypeDto));
        }

        [HttpPut]
        public async Task<IActionResult> Put(PaymentTypeUpdateDto paymentTypeDto)
        {
            return CustomActionResult(await _service.UpdateAsync(paymentTypeDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CustomActionResult(await _service.DeleteAsync(id));

        }
    }
}
