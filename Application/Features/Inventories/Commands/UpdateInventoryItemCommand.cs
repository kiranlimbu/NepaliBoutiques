using Application.Abstractions;
using Application.Features.Inventories.Models;

namespace Application.Features.Inventories.Commands;

public record UpdateInventoryItemCommand(InventoryItemModel Item) : ICommand;
