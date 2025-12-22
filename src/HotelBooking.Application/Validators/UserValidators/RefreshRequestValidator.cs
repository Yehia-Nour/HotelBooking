using FluentValidation;
using HotelBooking.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
