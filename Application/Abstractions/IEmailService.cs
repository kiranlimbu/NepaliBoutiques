using Core.Entities;
using Core.ValueObjects;


namespace Application.Abstractions;

/// <summary>
/// Defines the contract for sending emails in the application
/// </summary>
public interface IEmailService
{
    Task SendEmailAsync(Email recipient, string subject, string body);
}

