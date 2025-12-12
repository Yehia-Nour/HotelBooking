using HotelBooking.Infrastructure.Data.DataSeed.Implementations;
using HotelBooking.Infrastructure.Data.DataSeed.Interfaces;
using HotelBooking.Infrastructure.Data.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<HotelBookingDbContext>();

            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddScoped<IJsonFileReader, JsonFileReader>();
            services.AddKeyedScoped<IDataInitializer, DataInitializer>("Default");

            return services;
        }
    }
}
