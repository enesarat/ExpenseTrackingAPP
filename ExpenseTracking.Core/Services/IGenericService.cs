using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.Services
{
    public interface IGenericService<Entity,Dto> where Entity : BaseModel where Dto : class
    {
        Task<CustomResponse<Dto>> GetByIdAsync(int id);
        Task<CustomResponse<IEnumerable<Dto>>> GetAllAsync();
        Task<CustomResponse<IQueryable<Dto>>> WhereAsync(Expression<Func<Entity, bool>> expression);
        Task<CustomResponse<bool>> AnyAsync(Expression<Func<Entity, bool>> expression);
        Task<CustomResponse<NoContentResponse>> AddAsync(Dto item);
        Task<CustomResponse<NoContentResponse>> UpdateAsync(Dto item);
        Task<CustomResponse<NoContentResponse>> DeleteAsync(int id);
    }
}
