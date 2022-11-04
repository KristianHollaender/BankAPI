using System.Text;
using System.Text.Json.Serialization;
using Application;
using AutoMapper;
using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using Domain;
using FluentValidation;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

var mapperConfiguration = new MapperConfiguration(config =>
{
    config.CreateMap<PostCustomerDTO, Customer>();
    config.CreateMap<PutCustomerDTO, Customer>();
    config.CreateMap<PostAccountDTO, Account>();
    config.CreateMap<PutAccountDTO, Account>();
});

var mapper = mapperConfiguration.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(
    "Data source=db.db"
));
builder.Services.AddScoped<BankRepository>();


builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBankRepository, BankRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetValue<string>("AppSettings:Secret")))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", (policy) => { policy.RequireRole("Admin");});
});



builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   
}

app.UseCors(options => 
{
    options.SetIsOriginAllowed(origen => true)
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();