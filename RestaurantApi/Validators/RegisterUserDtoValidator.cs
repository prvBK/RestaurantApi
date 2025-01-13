using FluentValidation;
using RestaurantApi.Entities;
using RestaurantApi.Models;

namespace RestaurantApi.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    bool emailInUSe = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUSe)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
        }
    }
}