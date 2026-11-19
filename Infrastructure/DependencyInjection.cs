
using Application.Abstractions;
using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Interceptors;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
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
            services.AddDataProtection();
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
            }
            ).AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();


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
            services.AddScoped<IdentitySeeder>();

            return services;
        }
    }
}
