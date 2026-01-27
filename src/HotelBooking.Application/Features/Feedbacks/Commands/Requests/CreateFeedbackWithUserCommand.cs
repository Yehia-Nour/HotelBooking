using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Feedbacks.Commands.Requests
{
    public record CreateFeedbackWithUserCommand(string UserId, CreateFeedbackCommand Command) : IRequest<Result<int>>;
}
