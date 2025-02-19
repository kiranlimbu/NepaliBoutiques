using Core.Entities;

namespace Core.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    void Add(User user);
    void Update(User user);
    void Delete(User user);
}
