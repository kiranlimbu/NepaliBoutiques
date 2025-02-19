using MediatR;
using Application.Abstractions;
using Core.Events;
using Core.Abstractions.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Features.Users.EventHandlers;

public sealed class UserCreatedCoreEventHandler : INotificationHandler<UserCreatedCoreEvent>
{
    private readonly ILogger<UserCreatedCoreEventHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public UserCreatedCoreEventHandler(
        ILogger<UserCreatedCoreEventHandler> logger,
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _emailService = emailService;
    }
    
    public async Task Handle(UserCreatedCoreEvent notification, CancellationToken cancellationToken)
    {
        // get the user from the repository
        var user = await _userRepository.GetByIdAsync(notification.User.Id);
        if (user is null)
        {
            // this will never happen
            return;
        }

        // send email to the user
        await _emailService.SendEmailAsync(user.Email, "Welcome to the Nepali Style Finder", "You have successfully created an account");
    }
}
