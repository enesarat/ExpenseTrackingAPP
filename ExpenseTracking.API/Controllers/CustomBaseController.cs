using ExpenseTracking.API.Filters;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateFilterAttribute]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CustomActionResult<T>(CustomResponse<T> response)
        {
            if (response.StatusCode == 204)
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
