using Application.Abstractions;
using Core.ValueObjects;

namespace Infrastructure.ExternalServices;

public sealed class EmailService : IEmailService
{
    public Task SendEmailAsync(Email recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}
