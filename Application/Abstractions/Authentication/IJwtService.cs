using Core.Abstractions;

namespace Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<string>> GenerateTokenAsync(string email, string password, CancellationToken cancellationToken = default);
}
