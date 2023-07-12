using ExpenseTracking.Core.Models.Abstract;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ExpenseTracking.API.Filters
{
    public class UpdateUserIdSafetyFilter<T, Dto> : IAsyncActionFilter
    where T : Expense
    where Dto : class
    {
        private readonly IGenericRepository<T> _service;
        public UpdateUserIdSafetyFilter(IGenericRepository<T> service)
        {
            _service = service;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            Dto dto = context.ActionArguments.Values.FirstOrDefault(x => x is Dto) as Dto;

            var dtoInfo = (dto?.GetType().GetProperty("Id")?.GetValue(dto), dto?.GetType().GetProperty("UserId")?.GetValue(dto));
            var id = (int)dtoInfo.Item1;
            var userId = (int?)dtoInfo.Item2;


            var model = _service.GetByIdAsNoTrackingAsync(id).Result;


            var anyEntity = await _service.AnyAsync(x => x.Id == id && x.IsActive == true);
            if (model != null && dto != null && model.UserId != GetUserId(dto))
            {
                dto.GetType().GetProperty("UserId")?.SetValue(dto, model.UserId);

            }
            await next();

        }

        private int GetUserId(Dto dto)
        {
            var userIdProperty = dto.GetType().GetProperty("UserId");
            if (userIdProperty != null && userIdProperty.PropertyType == typeof(int))
            {
                return (int)userIdProperty.GetValue(dto);
            }

            return 0;
        }
    }
}
