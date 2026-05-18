namespace HotelBooking.Application.Features.Feedbacks.Queries.Requests
{
    public record GetAllFeedbacksQuery() : IRequest<Result<IEnumerable<FeedbackDTO>>>;
}
