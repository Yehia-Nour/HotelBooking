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
