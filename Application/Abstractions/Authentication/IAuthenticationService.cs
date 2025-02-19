namespace Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        string username, 
        string firstName, 
        string lastName, 
        string email, 
        string password, 
        string confirmPassword, 
        CancellationToken cancellationToken = default);
}
