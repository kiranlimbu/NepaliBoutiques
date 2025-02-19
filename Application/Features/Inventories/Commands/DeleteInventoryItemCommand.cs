using Application.Abstractions;

namespace Application.Features.Inventories.Commands;

public record DeleteInventoryItemCommand(int ItemId) : ICommand;


