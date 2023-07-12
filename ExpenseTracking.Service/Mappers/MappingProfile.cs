using AutoMapper;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.Expense;
using ExpenseTracking.Core.DTOs.Concrete.PaymentType;
using ExpenseTracking.Core.DTOs.Concrete.Role;
using ExpenseTracking.Core.DTOs.Concrete.User;
using ExpenseTracking.Core.Models.Concrete;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();

            CreateMap<ExpenseCreateDto, Expense>();
            CreateMap<ExpenseUpdateDto, Expense>();
            CreateMap<Expense, ExpenseDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType.Name))
            .ReverseMap();

            CreateMap<PaymentType, PaymentTypeDto>().ReverseMap();
            CreateMap<PaymentTypeCreateDto, PaymentType>();
            CreateMap<PaymentTypeUpdateDto, PaymentType>();

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<RoleCreateDto, Role>();
            CreateMap<RoleUpdateDto, Role>();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ReverseMap();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<UserUpdateAsAdminDto, User>();
        }
    }
}
