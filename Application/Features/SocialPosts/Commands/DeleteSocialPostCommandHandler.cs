using Application.Abstractions;
using Core.Entities;
using Core.Abstractions;
using Core.Abstractions.Repositories;
using Core.Errors;

namespace Application.Features.SocialPosts.Commands;

internal sealed class DeleteSocialPostCommandHandler : ICommandHandler<DeleteSocialPostCommand>
{
    private readonly ISocialPostRepository _socialPostRepository;
    private readonly IBoutiqueRepository _boutiqueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSocialPostCommandHandler(ISocialPostRepository socialPostRepository, IBoutiqueRepository boutiqueRepository, IUnitOfWork unitOfWork)
    {
        _socialPostRepository = socialPostRepository;
        _boutiqueRepository = boutiqueRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteSocialPostCommand request, CancellationToken cancellationToken)
    {
        // check if the post exists
        var post = await _socialPostRepository.GetByIdAsync(request.PostId);
        if (post is null)
        {
            return Result.Failure(SocialPostErrors.SocialPostNotFound);
        }
        // check if the boutique exists
        var boutique = await _boutiqueRepository.GetByIdAsync(post.BoutiqueId);
        if (boutique is null)
        {
            return Result.Failure(SocialPostErrors.BoutiqueNotFound);
        }
        // remove the post
        boutique.RemoveSocialPost(request.PostId);
        // save the changes
        _socialPostRepository.Delete(post.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

