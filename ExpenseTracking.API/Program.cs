using Autofac.Extensions.DependencyInjection;
using Autofac;
using ExpenseTracking.API.Filters;
using ExpenseTracking.API.Middlewares;
using ExpenseTracking.API.Modules;
using ExpenseTracking.Core.Repositories;
using ExpenseTracking.Core.Services;
using ExpenseTracking.Core.UnitOfWorks;
using ExpenseTracking.Repository.Contexts;
using ExpenseTracking.Repository.Repositories;
using ExpenseTracking.Repository.UnitOfWorks;
using ExpenseTracking.Service.Mappers;
using ExpenseTracking.Service.Services;
using ExpenseTracking.Service.Validations.Category;
using ExpenseTracking.Service.Validations.Expense;
using ExpenseTracking.Service.Validations.PaymentType;
using ExpenseTracking.Service.Validations.Role;
using ExpenseTracking.Service.Validations.User;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Logging;
using ExpenseTracking.Core.DTOs.Concrete.Expense;
using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.DTOs.Concrete.Role;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CategoryDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CategoryUpdateDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CategoryCreateDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ExpenseDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ExpenseCreateDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ExpenseUpdateDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<PaymentTypeDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<PaymentTypeCreateDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<PaymentTypeUpdateDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<RoleDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<RoleUpdateDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<RoleCreateDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserUpdateDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserUpdateAsAdminDtoValidator>())
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserCreateDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Update UserId Safety Filter Definitions
builder.Services.AddScoped<UpdateUserIdSafetyFilter<Expense, ExpenseUpdateDto>>();
#endregion


builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddDbContext<ApplicationDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("MsSql_ExpenseTracking_DB"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext)).GetName().Name);
    });
});

builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepositoryAndServiceModule()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
