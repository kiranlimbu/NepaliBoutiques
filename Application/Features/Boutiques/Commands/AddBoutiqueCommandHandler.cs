using Application.Abstractions;
using Core.Entities;
using Core.Abstractions;
using Core.Errors;
using Core.Abstractions.Repositories;
using Application.Common.Exceptions;

namespace Application.Features.Boutiques.Commands;

internal sealed class AddBoutiqueCommandHandler : ICommandHandler<AddBoutiqueCommand, int>
{
    private readonly IBoutiqueRepository _boutiqueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddBoutiqueCommandHandler(IBoutiqueRepository boutiqueRepository, IUnitOfWork unitOfWork)
    {
        _boutiqueRepository = boutiqueRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(AddBoutiqueCommand request, CancellationToken cancellationToken)
    {
        var boutique = await _boutiqueRepository.GetByNameAsync(request.Name, cancellationToken);
        if (boutique is not null)
        {
            return Result.Failure<int>(BoutiqueErrors.BoutiqueAlreadyExists);
        }
        // try catch block to handle concurrency exceptions (optimistic concurrency control), to prevent race conditions
        // though in this case, it is highly unlikely to happen (where two users try to add the same boutique name at the same time)
        // but it is a good practice to handle it
        try
        {
            var newBoutique = Boutique.Create(
            0, // Id will be set by database
            request.OwnerId,
            request.Name,
            request.ProfilePicture,
            request.Followers,
            request.Description,
            request.Category,
            request.Location,
            request.Contact,
            request.InstagramLink);

            _boutiqueRepository.Add(newBoutique);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(newBoutique.Id);
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<int>(BoutiqueErrors.BoutiqueAlreadyExists);
        }
    }
}
