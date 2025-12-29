using FluentValidation;
using HotelBooking.Application.DTOs.UserDTOs;

namespace HotelBooking.Application.Validators.UserValidators
{
    public class RefreshRequestValidator : AbstractValidator<RefreshRequestDTO>
    {
        public RefreshRequestValidator()
        {
            RuleFor(r => r.RefreshToken).RequiredField(nameof(RefreshRequestDTO.RefreshToken));
        }
    }
}
