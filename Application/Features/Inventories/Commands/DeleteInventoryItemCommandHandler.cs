using Application.Abstractions;
using Core.Entities;
using Core.Abstractions;
using Core.Abstractions.Repositories;
using Core.Errors;

namespace Application.Features.Inventories.Commands;

internal sealed class DeleteInventoryItemCommandHandler : ICommandHandler<DeleteInventoryItemCommand>
{
    private readonly IInventoryItemRepository _inventoryItemRepository;
    private readonly IBoutiqueRepository _boutiqueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInventoryItemCommandHandler(IInventoryItemRepository inventoryItemRepository, IBoutiqueRepository boutiqueRepository, IUnitOfWork unitOfWork)
    {
        _inventoryItemRepository = inventoryItemRepository;
        _boutiqueRepository = boutiqueRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteInventoryItemCommand request, CancellationToken cancellationToken)
    {
        // check if the item exists
        var item = await _inventoryItemRepository.GetByIdAsync(request.ItemId);
        if (item is null)
        {
            return Result.Failure(InventoryErrors.InventoryItemNotFound);
        }
        // check if the boutique exists
        var boutique = await _boutiqueRepository.GetByIdAsync(item.BoutiqueId);
        if (boutique is null)
        {
            return Result.Failure(InventoryErrors.BoutiqueNotFound);
        }
        // remove the item
        boutique.RemoveInventoryItem(request.ItemId);
        // save the changes
        _inventoryItemRepository.Delete(item.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}