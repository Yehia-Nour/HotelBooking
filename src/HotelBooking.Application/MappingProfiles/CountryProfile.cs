using AutoMapper;
using HotelBooking.Application.DTOs.CountryDTOs;
using HotelBooking.Application.Features.Countries.Commands.Requests;
using HotelBooking.Domain.Entities.Geography;

namespace HotelBooking.Application.MappingProfiles
{
    internal class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDTO>();

            CreateMap<CreateCountryCommand, Country>();

            CreateMap<UpdateCountryCommand, Country>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
