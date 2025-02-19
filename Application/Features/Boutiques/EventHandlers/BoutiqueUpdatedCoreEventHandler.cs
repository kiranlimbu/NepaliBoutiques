using MediatR;
using Core.Events;
using Core.Abstractions.Repositories;
using Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace Application.Features.Boutiques.EventHandlers;

public sealed class BoutiqueUpdatedCoreEventHandler : INotificationHandler<BoutiqueUpdatedCoreEvent>
{
    private readonly ILogger<BoutiqueUpdatedCoreEventHandler> _logger;
    private readonly IBoutiqueRepository _boutiqueRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public BoutiqueUpdatedCoreEventHandler(
        ILogger<BoutiqueUpdatedCoreEventHandler> logger,
        IBoutiqueRepository boutiqueRepository,
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _logger = logger;
        _boutiqueRepository = boutiqueRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(BoutiqueUpdatedCoreEvent notification, CancellationToken cancellationToken)
    {
        var boutique = await _boutiqueRepository.GetByIdAsync(notification.Boutique.Id);    
        if (boutique is null)
        {
            // this will never happen
            return;
        }

        var user = await _userRepository.GetByIdAsync(boutique.OwnerId ?? 0);
        if (user is null)
        {
            // todo: log the error
            return;
        }

        await _emailService.SendEmailAsync(user.Email, "Boutique Updated", $"Your boutique {boutique.Name} has been successfully updated");
    }
}
