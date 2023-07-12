using AutoMapper;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Expense;
using ExpenseTracking.Core.DTOs.Concrete.Response;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Repositories;
using ExpenseTracking.Core.Services;
using ExpenseTracking.Core.UnitOfWorks;
using ExpenseTracking.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Service.Services
{
    public class ExpenseService : GenericService<Expense, ExpenseDto>, IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserRepository _userRepository;
        public ExpenseService(IGenericRepository<Expense> repository, IUnitOfWork unitOfWork, IMapper mapper, IExpenseRepository expenseRepository, IUserRepository userRepository) : base(repository, unitOfWork, mapper)
        {
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
        }

        public async Task<CustomResponse<NoContentResponse>> AddAsync(ExpenseCreateDto expenseCreateDto)
        {
            if (await ExpenseVerifier(expenseCreateDto.Name))
            {
                return CustomResponse<NoContentResponse>.Fail(StatusCodes.Status400BadRequest, "This category name is registered in the system. Please specify another category name.");
            }
            var item = _mapper.Map<Expense>(expenseCreateDto);
            item.CreatedDate = DateTime.Now;
            item.TransactionDate= DateTime.Now;
            item.CreatedBy = "SYSTEM";
            await _expenseRepository.AddAsync(item);
            await _unitOfWork.CommitAsync();

            return CustomResponse<NoContentResponse>.Success(StatusCodes.Status204NoContent);
        }
        public async Task<bool> ExpenseVerifier(string name)
        {
            if (await _expenseRepository.AnyAsync(x => x.Name == name))
            {
                return true;
            }
            return false;
        }

        public async Task<CustomResponse<IEnumerable<ExpenseDto>>> GetExpensesWithDetailsAsync()
        {
            var entities = await _expenseRepository.GetExpensesWithDetails();
            var entitiesQueryable = entities.ToList().AsQueryable();
            var activeEntities = entitiesQueryable.Where(x => x.IsActive == true);

            var entitiesAsDto = _mapper.Map<IEnumerable<ExpenseDto>>(activeEntities);
            return CustomResponse<IEnumerable<ExpenseDto>>.Success(StatusCodes.Status200OK, entitiesAsDto);
        }

        public async Task<CustomResponse<ExpenseDto>> GetExpenseWithDetailsAsync(int id)
        {
            var entity = await _expenseRepository.GetExpenseWithDetails(id);
            if (entity != null && entity.IsActive != false)
            {
                var entityAsDto = _mapper.Map<ExpenseDto>(entity);

                return CustomResponse<ExpenseDto>.Success(StatusCodes.Status200OK, entityAsDto);
            }
            return CustomResponse<ExpenseDto>.Fail(StatusCodes.Status404NotFound, $" {typeof(Expense).Name} ({id}) not found. Retrieve operation is not successfull. ");

        }

        public async Task<CustomResponse<NoContentResponse>> UpdateAsync(ExpenseUpdateDto expenseUpdateDto)
        {
            if (await _expenseRepository.AnyAsync(x => x.Id == expenseUpdateDto.Id && x.IsActive == true))
            {
                var entity = _mapper.Map<Expense>(expenseUpdateDto);

                entity.UpdatedDate = DateTime.Now;
                _expenseRepository.Update(entity);
                await _unitOfWork.CommitAsync();
                return CustomResponse<NoContentResponse>.Success(StatusCodes.Status204NoContent);
            }
            return CustomResponse<NoContentResponse>.Fail(StatusCodes.Status404NotFound, $" {typeof(Category).Name} ({expenseUpdateDto.Id}) not found. Updete operation is not successfull. ");
        }
    }
}
