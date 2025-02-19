using Core.Entities;

namespace Core.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository that manages Boutique entities.
/// </summary>
public interface IBoutiqueRepository
{
    Task<Boutique?> GetByIdAsync(int id);
    Task<Boutique?> GetByNameAsync(string name);
    IEnumerable<Boutique> GetFeaturedBoutiques();
    Task<IEnumerable<Boutique>> GetAllAsync();
    void Add(Boutique boutique);
    void Update(Boutique boutique);
    void Delete(Boutique boutique);
}