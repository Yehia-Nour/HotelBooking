using HotelBooking.Application.DTOs.FeedbackDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Feedbacks.Queries.Requests
{
    public record GetAllFeedbacksQuery() : IRequest<Result<IEnumerable<FeedbackDTO>>>;
}
