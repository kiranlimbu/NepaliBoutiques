using Core.Entities;

namespace Core.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository that manages Boutique entities.
/// </summary>
public interface IBoutiqueRepository
{
    Task<Boutique?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Boutique?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Boutique>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Boutique boutique);
    void Update(Boutique boutique);
    void Delete(int id);
}