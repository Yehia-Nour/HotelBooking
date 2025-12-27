using AutoMapper;
using HotelBooking.Application.DTOs.RoomDTOs;
using HotelBooking.Application.Features.Rooms.Commands.Requests;
using HotelBooking.Domain.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
