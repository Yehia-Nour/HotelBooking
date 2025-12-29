namespace HotelBooking.Infrastructure.Data.DataSeed.Interfaces
{
    public interface IJsonFileReader
    {
        Task<List<T>> ReadJsonAsync<T>(string filePath);
    }
}
