using FluentValidation;
using HotelBooking.Application.Features.Refunds.Commands.Requests;

namespace HotelBooking.Application.Features.Refunds.Commands.Validators
{
    public class ProcessRefundCommandValidator : AbstractValidator<ProcessRefundCommand>
    {
        public ProcessRefundCommandValidator()
        {
            RuleFor(x => x.CancellationRequestId).GreaterThan(0);
            RuleFor(x => x.RefundMethodId).GreaterThan(0);
        }
    }
}
