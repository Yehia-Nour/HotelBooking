namespace HotelBooking.Application.MappingProfiles
{
    internal class RoomAmenityProfile : Profile
    {
        public RoomAmenityProfile()
        {
            CreateMap<RoomAmenity, AmenityDTO>().IncludeMembers(src => src.Amenity);

            CreateMap<RoomAmenity, RoomTypeDTO>().IncludeMembers(src => src.RoomType);
        }
    }
}
