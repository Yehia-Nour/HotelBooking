using HotelBooking.Infrastructure.Data.DataSeed.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HotelBooking.Infrastructure.Data.DataSeed.Implementations
{
    public class JsonFileReader : IJsonFileReader
    {
        private readonly ILogger<JsonFileReader> _logger;

        public JsonFileReader(ILogger<JsonFileReader> logger)
        {
            _logger = logger;
        }
        public async Task<List<T>> ReadJsonAsync<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"JSON File not found: {filePath}");

            try
            {
                await using var stream = File.OpenRead(filePath);

                var data = await JsonSerializer.DeserializeAsync<List<T>>(
                    stream,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters =
                        {
                            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                        }
                    }


                );

                return data ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while reading JSON file: {ex.Message}");
                return [];
            }
        }
    }
}
