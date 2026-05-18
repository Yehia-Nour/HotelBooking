namespace HotelBooking.Application.Features.Amenities.Queries.Requests
{
    public record GetAmenityByIdQuery(int AmenityId) : IRequest<Result<AmenityDTO>>;
}
