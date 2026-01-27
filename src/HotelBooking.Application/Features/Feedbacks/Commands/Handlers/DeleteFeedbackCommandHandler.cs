using HotelBooking.Application.Features.Feedbacks.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.FeedbackSpecifications;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.Feedbacks.Commands.Handlers
{
    public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackWithUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFeedbackCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteFeedbackWithUserCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Feedback>();

            var feedback = await repo.GetAsync([FeedbackCriteriaSpecification.ByIdForUser(request.FeedbackId, request.UserId)]);

            if (feedback is null)
                return Result.Fail(Error.Failure("Feedback.NotFound", "Feedback not found or not owned by user."));

            repo.Delete(feedback);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
