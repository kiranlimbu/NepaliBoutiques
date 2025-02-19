using Application.Abstractions;
using Core.Entities;

namespace Application.Features.Inventories.Commands;

/// <summary>
/// Command to add inventory items to a boutique.
/// </summary>
public class AddInventoryItemsCommand : ICommand<IEnumerable<int>>
{
    /// <summary>
    /// Gets the list of inventory items to be added.
    /// </summary>
    public List<InventoryItem> Items { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddInventoryItemsCommand"/> class with a single inventory item.
    /// </summary>
    /// <param name="singleItem">The single inventory item to add.</param>
    public AddInventoryItemsCommand(InventoryItem singleItem)
    {
        Items = [singleItem];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddInventoryItemsCommand"/> class with a list of inventory items.
    /// </summary>
    /// <param name="items">The list of inventory items to add.</param>
    public AddInventoryItemsCommand(List<InventoryItem> items)
    {
        Items = items;
    }
}
