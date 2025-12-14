using HotelBooking.Application.Services.Interfaces;
using HotelBooking.Domain.Contracts;
using HotelBooking.Infrastructure.Data.DataSeed.Implementations;
using HotelBooking.Infrastructure.Data.DataSeed.Interfaces;
using HotelBooking.Infrastructure.Data.DbContexts;
using HotelBooking.Infrastructure.Data.Identity;
using HotelBooking.Infrastructure.Data.Identity.Entities;
using HotelBooking.Infrastructure.Data.Identity.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace HotelBooking.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<HotelBookingDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<HotelBookingDbContext>();

            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddScoped<IJsonFileReader, JsonFileReader>();
            services.AddKeyedScoped<IDataInitializer, DataInitializer>("Default");

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            

            return services;
        }
    }
}
