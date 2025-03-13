using Application.Abstractions;
using Core.Entities;
using Core.Abstractions;
using Core.Abstractions.Repositories;
using Core.Errors;

namespace Application.Features.Inventories.Commands;


internal sealed class UpdateInventoryItemCommandHandler : ICommandHandler<UpdateInventoryItemCommand>
{
    private readonly IInventoryItemRepository _inventoryItemRepository;
    private readonly IBoutiqueRepository _boutiqueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInventoryItemCommandHandler(IInventoryItemRepository inventoryItemRepository, IBoutiqueRepository boutiqueRepository, IUnitOfWork unitOfWork)  
    {
        _inventoryItemRepository = inventoryItemRepository;
        _boutiqueRepository = boutiqueRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateInventoryItemCommand request, CancellationToken cancellationToken)
    {
        if (request.Item.Id is null)
        {
            return Result.Failure(InventoryErrors.InventoryItemIdRequired);
        }
        // check if the item exists
        var item = await _inventoryItemRepository.GetByIdAsync(request.Item.Id.Value, cancellationToken);
        if (item is null)
        {
            return Result.Failure(InventoryErrors.InventoryItemNotFound);
        }
        // check if the related boutique exists
        var boutique = await _boutiqueRepository.GetByIdAsync(item.BoutiqueId, cancellationToken);
        if (boutique is null)
        {
            return Result.Failure(InventoryErrors.BoutiqueNotFound);
        }
        // this creates a required data type for update
        var updatedItem = InventoryItem.Create(
            item.Id,
            item.BoutiqueId,
            request.Item.ImageUrl,
            request.Item.Caption);
        // update the item
        boutique.UpdateInventoryItem(updatedItem);
        // save the changes to the repository
        _inventoryItemRepository.Update(item);
        // save the changes to the database
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
