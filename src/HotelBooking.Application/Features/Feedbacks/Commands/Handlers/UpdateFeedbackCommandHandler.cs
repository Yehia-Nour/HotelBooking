using HotelBooking.Application.Features.Feedbacks.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.FeedbackSpecifications;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.Feedbacks.Commands.Handlers
{
    public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackWithUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFeedbackCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateFeedbackWithUserCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var cmd = request.Command;

            var repo = _unitOfWork.GetRepository<Feedback>();

            var feedback = await repo.GetAsync([FeedbackCriteriaSpecification.ByIdForUser(cmd.FeedbackId, userId)]);

            if (feedback is null)
                return Result.Fail(Error.Failure("Feedback.NotFound", "Feedback not found or not owned by user."));

            feedback.Rating = cmd.Rating;
            feedback.Comment = cmd.Comment;
            feedback.FeedbackDate = DateTime.UtcNow;

            repo.Update(feedback);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
