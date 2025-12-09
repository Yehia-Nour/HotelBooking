using HotelBooking.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Presentation.Extensions
{
    public static class DatabaseMigrationExtensions
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContextService = scope.ServiceProvider.GetRequiredService<HotelBookingDbContext>();
            var pendingMigrations = await dbContextService.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await dbContextService.Database.MigrateAsync();

            return app;
        }
    }
}
