using FluentValidation;
using HotelBooking.Application.Features.Feedbacks.Commands.Requests;

namespace HotelBooking.Application.Features.Feedbacks.Commands.Validators
{
    public class UpdateFeedbackCommandValidator : AbstractValidator<UpdateFeedbackCommand>
    {
        public UpdateFeedbackCommandValidator()
        {
            RuleFor(x => x.FeedbackId).GreaterThan(0);
            RuleFor(x => x.Rating).InclusiveBetween(1, 5);
            RuleFor(x => x.Comment).MaximumLength(2000);
        }
    }
}
