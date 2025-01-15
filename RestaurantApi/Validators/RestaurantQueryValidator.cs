using FluentValidation;
using RestaurantApi.Entities;
using RestaurantApi.Models;

namespace RestaurantApi.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private int[] allowPageSizes = { 5, 10, 15 };
        private string[] allowSortByColumnNames = { nameof(Restaurant.Name), nameof(Restaurant.Description), nameof(Restaurant.Category) };

        public RestaurantQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(", ", allowPageSizes)}]");
                }
            });

            RuleFor(r => r.SortBy).Custom((value, context) =>
            {
                if (!(value is null || allowSortByColumnNames.Contains(value)))
                {
                    context.AddFailure("SortBy", $"SortBy must in [{string.Join(", ", allowSortByColumnNames)}]");
                }
            });

        }
    }
}