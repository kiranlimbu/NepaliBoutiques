using Core.Entities;

namespace Core.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository that manages SocialPost entities.
/// </summary>
public interface ISocialPostRepository
{
    Task<SocialPost?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<SocialPost>> GetByBoutiqueIdAsync(int boutiqueId, CancellationToken cancellationToken = default);
    void Delete(int id);
}
