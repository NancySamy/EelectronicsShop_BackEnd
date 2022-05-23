using LinkDevelopmentWorkshop.Application.Services;
using LinkDevelopmentWorkshop.Repo.CustomRepo;
using LinkDevelopmentWorkshop.Repo.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LinkDevelopmentWorkshop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
           services.AddScoped<IUnitOfWork, UnitOfWork>();
           services.AddScoped<IUserRepository, UserRepository>();
           services.AddScoped<IProductRepository, ProductRepository>();
           services.AddScoped<IOrderRepository, OrderRepository>();
           services.AddScoped<ICategoryRepository, CategoryRepository>();
           services.AddScoped<JwtService>();
           services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
