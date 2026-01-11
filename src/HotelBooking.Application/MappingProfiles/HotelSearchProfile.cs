using AutoMapper;
using HotelBooking.Application.DTOs.HotelSearchDTOs;
using HotelBooking.Domain.Entities.Rooms;

namespace HotelBooking.Application.MappingProfiles
{
    internal class HotelSearchProfile : Profile
    {
        public HotelSearchProfile()
        {
            CreateMap<RoomType, RoomTypeSearchDTO>();

            CreateMap<Room, RoomSearchDTO>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

            CreateMap<Amenity, AmenitySearchDTO>();

            CreateMap<RoomType, RoomTypeSearchWithAmenitiesDTO>()
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.RoomAmenities.Select(ra => ra.Amenity)));

            CreateMap<Room, RoomSearchWithAmenitiesDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
