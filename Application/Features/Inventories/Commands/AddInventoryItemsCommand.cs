using Application.Abstractions;
using Application.Features.Inventories.Models;

namespace Application.Features.Inventories.Commands;

/// <summary>
/// Command to add inventory items to a boutique.
/// </summary>
public class AddInventoryItemsCommand : ICommand<IEnumerable<int>>
{
    /// <summary>
    /// Gets the list of inventory items to be added.
    /// </summary>
    public List<InventoryItemModel> Items { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddInventoryItemsCommand"/> class with a list of inventory items.
    /// </summary>
    /// <param name="items">The list of inventory items to add.</param>
    public AddInventoryItemsCommand(List<InventoryItemModel> items)
    {
        Items = items;
    }
}
