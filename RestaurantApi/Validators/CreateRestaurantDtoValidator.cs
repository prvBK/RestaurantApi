using FluentValidation;
using RestaurantApi.Models;

namespace RestaurantApi.Validators
{
    public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
    {
        public CreateRestaurantDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(25)
                .MinimumLength(5);

            RuleFor(x => x.ContactEmail)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Street)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}