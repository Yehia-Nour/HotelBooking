using HotelBooking.Application.DTOs.FeedbackDTOs;
using HotelBooking.Application.Results;
using MediatR;

namespace HotelBooking.Application.Features.Feedbacks.Queries.Requests
{
    public record GetFeedbackByIdQuery(int FeedbackId) : IRequest<Result<FeedbackDTO>>;
}
