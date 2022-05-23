
using FluentValidation.AspNetCore;
using LinkDevelopmentWorkshop.Application;
using LinkDevelopmentWorkshop.Application.Services;
using LinkDevelopmentWorkshop.Application.Users.Commands.CreateUser;
using LinkDevelopmentWorkshop.Repo.Context;
using LinkDevelopmentWorkshop.Repo.CustomRepo;
using LinkDevelopmentWorkshop.Repo.UnitOfWork;
using LinkDevelopmentWorkshop.Security.Helpers;
using LinkDevelopmentWorkshop.Security.IOC;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>())
    .AddJsonOptions(op =>
{
    op.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddCors();
builder.Services.AddDbContext<ApplicationDBContext>(op=>op.UseSqlServer(builder.Configuration.GetConnectionString("LinkDevelopmentTaskDbConnection")));
builder.Services.AddApplication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinkDevelopmentWorkShop", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { 
    Description="Jwt Authorization",
    Name ="Authorization",
    In  =ParameterLocation.Header,
    Type =SecuritySchemeType.ApiKey,
    Scheme ="Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer",
                }
            },
            new string[]{}
        }
    });
});
//builder.Services.HandleCustomAuthentication();
builder.Services.AddAuthenticationSupport();
builder.Services.AddAuthorizationSupport();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(op =>
//    op.TokenValidationParameters = new TokenValidationParameters { 
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//       ValidIssuer=builder.Configuration["Jwt:Issuer"],
//       ValidAudience=builder.Configuration["Jwt:Audience"],
//       IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//}); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","LinkDevelopmentWorkShop v1"));
}

app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(op => op.WithOrigins(new[] { "http://localhost:3000", "http://localhost:3001", "http://localhost:8080", "http://localhost:4200" }).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
app.MapControllers();

app.Run();
