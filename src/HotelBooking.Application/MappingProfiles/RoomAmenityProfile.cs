using AutoMapper;
using HotelBooking.Application.DTOs.AmenityDTOs;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Domain.Entities.Rooms;

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
