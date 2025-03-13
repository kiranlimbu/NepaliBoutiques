using FluentValidation;
using Application.Features.Boutiques.Commands;

namespace Application.Features.Boutiques.Validators;

public class AddBoutiqueCommandValidator : AbstractValidator<AddBoutiqueCommand>
{
    public AddBoutiqueCommandValidator()
    {
        RuleFor(b => b.OwnerId)
            .NotEmpty()
            .GreaterThan(0).WithMessage("OwnerId cannot be zero."); // check for 0, which is not allowed
        RuleFor(b => b.Name)
            .NotEmpty() // check for null or empty, both are not allowed
            .MaximumLength(150);

        RuleFor(b => b.ProfilePicture)
            .NotEmpty(); // check for null or empty, both are not allowed
            
        RuleFor(b => b.Description)
            .NotNull() // check for only null, empty "" is allowed
            .MaximumLength(1000);

        RuleFor(b => b.Category)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(b => b.Location)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(b => b.Contact)
            .NotNull() // check for only null, empty "" is allowed
            .MaximumLength(200);

    }
}
