namespace HotelBooking.Application.Features.Amenities.Queries.Requests
{
    public record GetAllAmenitiesQuery(bool? IsActive) : IRequest<Result<IEnumerable<AmenityDTO>>>;
}
