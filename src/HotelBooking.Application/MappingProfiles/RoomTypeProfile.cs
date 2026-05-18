namespace HotelBooking.Application.MappingProfiles
{
    internal class RoomTypeProfile : Profile
    {
        public RoomTypeProfile()
        {
            CreateMap<RoomType, RoomTypeDTO>();

            CreateMap<CreateRoomTypeCommand, RoomType>();

            CreateMap<UpdateRoomTypeCommand, RoomType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
