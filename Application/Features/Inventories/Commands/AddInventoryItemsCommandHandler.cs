using Application.Abstractions;
using Core.Entities;
using Core.Abstractions;
using Core.Abstractions.Repositories;
using Core.Errors;

namespace Application.Features.Inventories.Commands;

internal sealed class AddInventoryItemsCommandHandler : ICommandHandler<AddInventoryItemsCommand, IEnumerable<int>>
{
    private readonly IInventoryItemRepository _inventoryItemRepository;
    private readonly IBoutiqueRepository _boutiqueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddInventoryItemsCommandHandler(IInventoryItemRepository inventoryItemRepository, IBoutiqueRepository boutiqueRepository, IUnitOfWork unitOfWork)
    {
        _inventoryItemRepository = inventoryItemRepository;
        _boutiqueRepository = boutiqueRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the addition of inventory item(s) to a boutique.
    /// </summary>
    /// <param name="request">The command containing the inventory items to add.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A Result containing the IDs of the added inventory items or an error.</returns>
    public async Task<Result<IEnumerable<int>>> Handle(AddInventoryItemsCommand request, CancellationToken cancellationToken)
    {
        // Attempt to retrieve the boutique by its ID
        var boutique = await _boutiqueRepository.GetByIdAsync(request.Items.First().BoutiqueId, cancellationToken);
        if (boutique == null)
        {
            // Return failure if the boutique is not found
            return Result.Failure<IEnumerable<int>>(InventoryErrors.BoutiqueNotFound);
        }

        var addedItems = new List<InventoryItem>();
        var errors = new List<string>();

        // Iterate over each inventory item in the request
        foreach (var item in request.Items)
        {
            // Create a new inventory item with a temporary ID
            var inventoryItem = new InventoryItem(
                0, // Id will be set by database
                item.BoutiqueId, 
                item.ImageUrl, 
                item.Caption, 
                DateTime.UtcNow);

            // Attempt to add the inventory item to the boutique
            var result = boutique.AddInventoryItem(inventoryItem);
            if (result.IsFailure)
            {
                // Collect error codes for failed additions
                errors.Add(result.Error.Code);
                continue;
            }

            // Add successfully created inventory items to the list
            addedItems.Add(inventoryItem);
        }

        // Check if no items were successfully added
        if (addedItems.Count == 0)
        {
            return Result.Failure<IEnumerable<int>>(InventoryErrors.NoItemsAdded);
        }
        // Batch add items to the repository
        await _inventoryItemRepository.AddRangeAsync(addedItems, cancellationToken);
        // Commit the transaction
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (errors.Count != 0)
        {
            // TODO: Log the errors
        }

        // Return the IDs of the successfully added inventory items
        return Result.Success(addedItems.Select(i => i.Id));
    }
}
