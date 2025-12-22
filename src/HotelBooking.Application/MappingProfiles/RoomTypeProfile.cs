using AutoMapper;
using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Domain.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.MappingProfiles
{
    internal class RoomTypeProfile : Profile
    {
        public RoomTypeProfile()
        {
            CreateMap<RoomType, RoomTypeDTO>();
            CreateMap<CreateRoomTypeDTO, RoomType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
