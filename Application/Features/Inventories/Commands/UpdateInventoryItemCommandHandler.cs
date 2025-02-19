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
        // todo: validate the image url and caption
        // update the item
        boutique.UpdateInventoryItem(request.ItemId, request.NewImageUrl, request.NewCaption);
        // save the changes
        _inventoryItemRepository.Update(item);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
