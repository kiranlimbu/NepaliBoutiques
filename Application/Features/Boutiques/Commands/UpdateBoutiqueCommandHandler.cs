using Application.Abstractions;
using Core.Entities;
using Core.Abstractions;
using Core.Abstractions.Repositories;
using Core.Errors;

namespace Application.Features.Boutiques.Commands;

internal sealed class UpdateBoutiqueCommandHandler : ICommandHandler<UpdateBoutiqueCommand>
{
    private readonly IBoutiqueRepository _boutiqueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBoutiqueCommandHandler(IBoutiqueRepository boutiqueRepository, IUnitOfWork unitOfWork)
    {
        _boutiqueRepository = boutiqueRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateBoutiqueCommand request, CancellationToken cancellationToken)
    {
        // check if the boutique exists
        var boutique = await _boutiqueRepository.GetByIdAsync(request.Id, cancellationToken);
        if (boutique is null)
        {
            return Result.Failure(BoutiqueErrors.BoutiqueNotFound);
        }
        // this creates a required data type for update
        var updatedBoutique = Boutique.Create(
            boutique.Id,
            request.OwnerId,
            request.Name,
            request.ProfilePicture,
            request.Followers,
            request.Description,
            request.Category,
            request.Location,
            request.Contact,
            request.InstagramLink);

        boutique.UpdateBoutique(updatedBoutique);
        // this updates the boutique
        _boutiqueRepository.Update(boutique);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
