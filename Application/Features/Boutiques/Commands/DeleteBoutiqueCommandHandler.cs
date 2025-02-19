using MediatR;
using Application.Abstractions;
using Core.Entities;
using Core.Abstractions;
using Core.Abstractions.Repositories;
using Core.Errors;
using Core.Events;

namespace Application.Features.Boutiques.Commands;

internal sealed class DeleteBoutiqueCommandHandler : ICommandHandler<DeleteBoutiqueCommand>
{
    private readonly IBoutiqueRepository _boutiqueRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public DeleteBoutiqueCommandHandler(IBoutiqueRepository boutiqueRepository, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _boutiqueRepository = boutiqueRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<Result> Handle(DeleteBoutiqueCommand request, CancellationToken cancellationToken)
    {
        var boutique = await _boutiqueRepository.GetByIdAsync(request.Id);
        if (boutique is null)
        {
            return Result.Failure(BoutiqueErrors.BoutiqueNotFound);
        }

        _boutiqueRepository.Delete(boutique);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Raise the event
        await _mediator.Publish(new BoutiqueDeletedCoreEvent(boutique), cancellationToken);

        return Result.Success();
    }
}

