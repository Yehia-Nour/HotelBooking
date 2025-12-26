using HotelBooking.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Features.RoomTypes.Commands.Requests
{
    public record UpdateRoomTypeWithUserCommand(UpdateRoomTypeCommand Command, string UserEmail) : IRequest<Result>;
}
