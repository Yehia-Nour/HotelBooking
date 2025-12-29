using AutoMapper;
using HotelBooking.Application.DTOs.AmenityDTOs;
using HotelBooking.Application.Features.Amenities.Commands.Requests;
using HotelBooking.Domain.Entities.Rooms;

namespace HotelBooking.Application.MappingProfiles
{
    internal class AmenityProfile : Profile
    {
        public AmenityProfile()
        {
            CreateMap<Amenity, AmenityDTO>();

            CreateMap<CreateAmenityCommand, Amenity>();

            CreateMap<UpdateAmenityCommand, Amenity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
