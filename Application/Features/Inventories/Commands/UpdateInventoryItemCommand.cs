using Application.Abstractions;

namespace Application.Features.Inventories.Commands;

public record UpdateInventoryItemCommand(int ItemId, string? NewImageUrl = null, string? NewCaption = null) : ICommand;
