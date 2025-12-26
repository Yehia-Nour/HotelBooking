using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Features.RoomTypes.Queries.Requests
{
    public record GetRoomTypeByIdQuery(int Id) : IRequest<Result<RoomTypeDTO>>;
}
