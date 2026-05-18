namespace HotelBooking.Application.Features.Feedbacks.Queries.Requests
{
    public record GetFeedbackByIdQuery(int FeedbackId) : IRequest<Result<FeedbackDTO>>;
}
