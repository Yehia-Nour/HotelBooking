using FluentValidation;
using HotelBooking.Application.Features.Feedbacks.Commands.Requests;

namespace HotelBooking.Application.Features.Feedbacks.Commands.Validators
{
    public class CreateFeedbackCommandValidator : AbstractValidator<CreateFeedbackCommand>
    {
        public CreateFeedbackCommandValidator()
        {
            RuleFor(x => x.ReservationId).GreaterThan(0);
            RuleFor(x => x.Rating).InclusiveBetween(1, 5);
            RuleFor(x => x.Comment).MaximumLength(2000);
        }
    }
}
