namespace HotelBooking.Application.Validators.UserValidators
{
    public class UserRoleValidator : AbstractValidator<UserRoleDTO>
    {
        public UserRoleValidator()
        {
            RuleFor(ur => ur.UserId).RequiredField(nameof(UserRoleDTO.UserId));
        }
    }
}
