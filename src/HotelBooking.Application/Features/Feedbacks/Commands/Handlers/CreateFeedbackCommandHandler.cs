using HotelBooking.Application.Features.Feedbacks.Commands.Requests;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Results;
using HotelBooking.Application.Specifications.FeedbackSpecifications;
using HotelBooking.Application.Specifications.HotelBookingSpecifications;
using HotelBooking.Domain.Entities.Reservations;
using MediatR;

namespace HotelBooking.Application.Features.Feedbacks.Commands.Handlers
{
    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackWithUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateFeedbackCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateFeedbackWithUserCommand request, CancellationToken ct)
        {
            var userId = request.UserId;
            var cmd = request.Command;

            var reservationRepo = _unitOfWork.GetRepository<Reservation>();

            var reservation = await reservationRepo.GetAsync([HotelBookingReservationCriteriaSpecification.ByIdForUser(cmd.ReservationId, userId)]);

            if (reservation is null)
                return Error.Failure("Reservation.NotFound", "Reservation not found or not owned by user.");

            if (DateTime.UtcNow.Date < reservation.CheckOutDate.Date)
                return Error.Failure("Feedback.NotAllowed", "Feedback allowed only after checkout.");

            var feedbackRepo = _unitOfWork.GetRepository<Feedback>();

            var exists = await feedbackRepo.GetAsync([FeedbackCriteriaSpecification.ByReservationForUser(cmd.ReservationId, userId)]);

            if (exists is not null)
                return Error.Failure("Feedback.Exists", "Feedback already exists for this reservation.");

            var feedback = new Feedback
            {
                ReservationID = cmd.ReservationId,
                UserId = userId,
                Rating = cmd.Rating,
                Comment = cmd.Comment,
                FeedbackDate = DateTime.UtcNow
            };

            await feedbackRepo.AddAsync(feedback);
            await _unitOfWork.SaveChangesAsync();

            return feedback.Id;
        }
    }
}
