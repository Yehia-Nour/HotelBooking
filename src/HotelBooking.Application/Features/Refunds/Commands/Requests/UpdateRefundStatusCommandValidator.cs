using FluentValidation;
using HotelBooking.Domain.Entities.Payments;

namespace HotelBooking.Application.Features.Refunds.Commands.Requests
{
    public class UpdateRefundStatusCommandValidator : AbstractValidator<UpdateRefundStatusCommand>
    {
        public UpdateRefundStatusCommandValidator()
        {
            RuleFor(x => x.RefundId).GreaterThan(0);

            RuleFor(x => x.NewRefundStatus)
                .Must(s => s is RefundStatus.Pending
                           or RefundStatus.Processed
                           or RefundStatus.Completed
                           or RefundStatus.Failed)
                .WithMessage("Invalid new refund status provided.");
        }
    }
}
