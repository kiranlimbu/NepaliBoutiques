namespace Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        string firstName, 
        string lastName, 
        string email, 
        string password, 
        CancellationToken cancellationToken = default);
}
