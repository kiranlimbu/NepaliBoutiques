using Application.Abstractions;
using Core.Entities;

namespace Application.Features.Inventories.Commands;

public record UpdateInventoryItemCommand(InventoryItem Item) : ICommand;
