using HotelBooking.Presentation.Hubs;

namespace HotelBooking.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddPresentationServices(builder.Configuration);

            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddApplicationServices();


            var app = builder.Build();

            await app.MigrateDatabaseAsync();
            await app.SeedDatabaseAsync();
            await app.SeedIdentityDatabaseAsync();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<ReservationHub>("/hubs/reservation");

            app.Run();
        }
    }
}
