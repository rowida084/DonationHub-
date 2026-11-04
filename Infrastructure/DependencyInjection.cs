
using Application.Abstractions;
using Application.Abstractions.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<SoftDeleteInterceptor>();

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));

                options.AddInterceptors(
                    serviceProvider.GetRequiredService<SoftDeleteInterceptor>());
            });

            services.AddScoped(
                typeof(IGenericRepository<>),
                typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
