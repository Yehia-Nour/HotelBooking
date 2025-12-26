using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Features.RoomTypes.Commands.Requests
{
    public record CreateRoomTypeWithUserCommand(CreateRoomTypeCommand Command, string UserEmail) : IRequest<Result<int>>;

}
