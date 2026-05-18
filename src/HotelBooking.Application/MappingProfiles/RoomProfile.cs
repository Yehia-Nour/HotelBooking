namespace HotelBooking.Application.MappingProfiles
{
    internal class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDTO>();

            CreateMap<CreateRoomCommand, Room>();

            CreateMap<UpdateRoomCommand, Room>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
