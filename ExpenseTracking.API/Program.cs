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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ExpenseTracking.Core.DTOs.Concrete.Category;
using ExpenseTracking.Core.DTOs.Concrete.User;
using System.Net.Sockets;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using ExpenseTracking.Core.DTOs.Concrete.PaymentType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


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

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0.0",
        Title = "EventMate RESTful API",
        Contact = new OpenApiContact
        {
            Name = "Enes Arat",
            Url = new Uri("https://github.com/enesarat"),
            Email = "enes_arat@outlook.com"
        },
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

#region Update UserId Safety Filter Definitions
builder.Services.AddScoped<UpdateUserIdSafetyFilter<Expense, ExpenseUpdateDto>>();
builder.Services.AddScoped<CreateDateSafetyFilter<Expense, ExpenseUpdateDto>>();
builder.Services.AddScoped<CreatedBySafetyFilter<Expense, ExpenseUpdateDto>>();
#endregion
#region CreatedDate Filter Definitions
builder.Services.AddScoped<CreateDateSafetyFilter<Category, CategoryUpdateDto>>();
builder.Services.AddScoped<CreatedBySafetyFilter<Category, CategoryUpdateDto>>();

builder.Services.AddScoped<CreateDateSafetyFilter<Role, RoleUpdateDto>>();
builder.Services.AddScoped<CreatedBySafetyFilter<Role, RoleUpdateDto>>();


builder.Services.AddScoped<CreateDateSafetyFilter<User, UserUpdateDto>>();
builder.Services.AddScoped<CreatedBySafetyFilter<User, UserUpdateDto>>();

builder.Services.AddScoped<CreateDateSafetyFilter<User, UserUpdateAsAdminDto>>();
builder.Services.AddScoped<CreatedBySafetyFilter<User, UserUpdateAsAdminDto>>();

builder.Services.AddScoped<CreateDateSafetyFilter<PaymentType, PaymentTypeUpdateDto>>();
builder.Services.AddScoped<CreatedBySafetyFilter<PaymentType, PaymentTypeUpdateDto>>();
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


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
