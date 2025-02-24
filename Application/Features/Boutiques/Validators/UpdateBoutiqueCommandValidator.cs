using FluentValidation;
using Application.Features.Boutiques.Commands;

namespace Application.Features.Boutiques.Validators;

public class UpdateBoutiqueCommandValidator : AbstractValidator<UpdateBoutiqueCommand>
{
    public UpdateBoutiqueCommandValidator()
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
            .NotNull(); // check for only null, empty "" is allowed

        RuleFor(b => b.Category)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(b => b.Location)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(b => b.Contact)
            .NotNull() // check for only null, empty "" is allowed
            .MaximumLength(150);
    }
}