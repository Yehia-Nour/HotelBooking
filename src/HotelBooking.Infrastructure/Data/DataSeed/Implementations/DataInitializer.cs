using HotelBooking.Domain.Contracts;
using HotelBooking.Domain.Entities.Common;
using HotelBooking.Infrastructure.Data.DataSeed.Interfaces;
using HotelBooking.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace HotelBooking.Infrastructure.Data.DataSeed.Implementations
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IDataSeeder _dataSeeder;
        private readonly IJsonFileReader _jsonFileReader;
        private readonly ILogger<DataInitializer> _logger;
        private readonly HotelBookingDbContext _dbContext;
        private readonly string _path = @"..\HotelBooking.Infrastructure\Data\DataSeed\JSONFiles\";

        public DataInitializer(IDataSeeder dataSeeder,
            IJsonFileReader jsonFileReader,
            ILogger<DataInitializer> logger,
            HotelBookingDbContext dbContext)
        {
            _dataSeeder = dataSeeder;
            _jsonFileReader = jsonFileReader;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var hasAmenities = await _dbContext.Amenities.AnyAsync();
                var hasCountries = await _dbContext.Countries.AnyAsync();
                var hasRefundMethods = await _dbContext.RefundMethods.AnyAsync();
                var hasRoomTypes = await _dbContext.RoomTypes.AnyAsync();
                var hasRoomAmenities = await _dbContext.RoomAmenities.AnyAsync();
                var hasRooms = await _dbContext.Rooms.AnyAsync();
                var hasStates = await _dbContext.States.AnyAsync();

                if (hasAmenities && hasCountries && hasRefundMethods && hasRoomTypes && hasRoomAmenities && hasRooms && hasStates) return;

                if (!hasAmenities)
                {
                    await SeedJsonFile("Amenities.json", _dbContext.Amenities);
                    _logger.LogInformation("Seeding Amenities completed.");
                }

                if (!hasCountries)
                {
                    await SeedJsonFile("Countries.json", _dbContext.Countries);
                    _logger.LogInformation("Seeding Countries completed.");
                }

                if (!hasRefundMethods)
                {
                    await SeedJsonFile("RefundMethods.json", _dbContext.RefundMethods);
                    _logger.LogInformation("Seeding RefundMethods completed.");
                }

                if (!hasRoomTypes)
                {
                    await SeedJsonFile("RoomTypes.json", _dbContext.RoomTypes);
                    _logger.LogInformation("Seeding RoomTypes completed.");
                }

                await _dbContext.SaveChangesAsync();

                if (!hasRoomAmenities)
                {
                    await SeedJsonFile("RoomAmenities.json", _dbContext.RoomAmenities);
                    _logger.LogInformation("Seeding RoomAmenities completed.");
                }

                if (!hasRooms)
                {
                    await SeedJsonFile("Rooms.json", _dbContext.Rooms);
                    _logger.LogInformation("Seeding Rooms completed.");
                }

                if (!hasStates)
                {
                    await SeedJsonFile("States.json", _dbContext.States);
                    _logger.LogInformation("Seeding States completed.");
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Data Seeding Failed: {ex.Message}");
            }
        }

        private async Task SeedJsonFile<T>(string fileName, DbSet<T> dbSet) where T : BaseEntity
        {
            var filePath = Path.Combine(_path, fileName);

            var data = await _jsonFileReader.ReadJsonAsync<T>(filePath);

            await _dataSeeder.SeedAsync(dbSet, data);
        }
    }
}
