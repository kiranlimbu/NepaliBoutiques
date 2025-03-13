using FluentValidation;
using Application.Features.Inventories.Commands;
using Application.Features.Inventories.Models;
namespace Application.Features.Inventories.Validators;

/// <summary>
/// Validator for AddInventoryItemsCommand to ensure the command is valid.
/// </summary>
public class AddInventoryItemsCommandValidator : AbstractValidator<AddInventoryItemsCommand>
{
    public AddInventoryItemsCommandValidator()
    {
        RuleFor(i => i.Items)
            .NotEmpty()
            .ForEach(item =>
            {
                item.NotNull();
                item.SetValidator(new InventoryItemValidator());
            });
    }
}

/// <summary>
/// Validator for individual InventoryItem.
/// </summary>
public class InventoryItemValidator : AbstractValidator<InventoryItemModel>
{
    public InventoryItemValidator()
    {
        RuleFor(i => i.ImageUrl)
            .NotEmpty() // check for null or empty, both are not allowed
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute)).WithMessage("Image URL must be a valid URL.");
    }
}