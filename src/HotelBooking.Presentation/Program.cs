
using HotelBooking.Application.DependencyInjection;
using HotelBooking.Infrastructure.DependencyInjection;
using HotelBooking.Presentation.CustomMiddlewares;
using HotelBooking.Presentation.Extensions;
using System.Threading.Tasks;

namespace HotelBooking.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddApplicationServices();



            var app = builder.Build();

            #region Data Seeding
            await app.MigrateDatabaseAsync();
            await app.SeedDatabaseAsync(); 
            await app.SeedIdentityDatabaseAsync();
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
