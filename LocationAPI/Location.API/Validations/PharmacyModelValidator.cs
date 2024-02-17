using FluentValidation;
using Location.API.Models;

namespace Location.API.Validations;

public class PharmacyModelValidator : AbstractValidator<PharmacyModel>
{
    public PharmacyModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.OpenningTime).Custom((openningTime, context) =>
        {

            var model = context.InstanceToValidate;
            if (!TimeSpan.TryParse(openningTime, out TimeSpan openningTimeSpan))
            {
                context.AddFailure("Invalid opening time");
                return;
            }
        });

        RuleFor(x => x.ClosingTime).Custom((closingTime, context) =>
        {

            var model = context.InstanceToValidate;
            if (!TimeSpan.TryParse(closingTime, out TimeSpan closingTimeSpan))
            {
                context.AddFailure("Invalid closing time");
                return;
            }
        });

        RuleFor(x => x.ClosingTime)
    .Custom((closingTime, context) => {

        var model = context.InstanceToValidate;
        if (!TimeSpan.TryParse(closingTime, out TimeSpan closingTimeSpan))
        {
            context.AddFailure("Invalid closing time");
            return;
        }

        if (!TimeSpan.TryParse(model.OpenningTime, out TimeSpan openingTimeSpan))
        {
            context.AddFailure("Invalid opening time");
            return;
        }

        if (closingTimeSpan <= openingTimeSpan)
        {
            context.AddFailure("Closing time must be greater than opening time");
        }
    });
    }

    private bool BeValidTimeSpan(string time)
    {
        if (TimeSpan.TryParse(time, out TimeSpan timeSpan))
        {
            return timeSpan >= TimeSpan.Zero && timeSpan < TimeSpan.FromDays(1);
        }

        return false;
    }

}