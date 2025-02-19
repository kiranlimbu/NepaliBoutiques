using MediatR;
using Core.Events;
using Core.Abstractions.Repositories;
using Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace Application.Features.Inventories.EventHandlers;

public sealed class InventoryItemRemovedCoreEventHandler : INotificationHandler<InventoryItemRemovedCoreEvent>
{
    private readonly ILogger<InventoryItemRemovedCoreEventHandler> _logger;
    private readonly IBoutiqueRepository _boutiqueRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public InventoryItemRemovedCoreEventHandler(
        ILogger<InventoryItemRemovedCoreEventHandler> logger,
        IBoutiqueRepository boutiqueRepository,
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _logger = logger;
        _boutiqueRepository = boutiqueRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    /// <summary>
    /// Handles the InventoryItemRemovedCoreEvent by sending an email notification to the boutique owner.
    /// </summary>
    /// <param name="notification">The event notification containing the inventory item details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    public async Task Handle(InventoryItemRemovedCoreEvent notification, CancellationToken cancellationToken)
    {
        // Retrieve the boutique by its ID
        var boutique = await _boutiqueRepository.GetByIdAsync(notification.Item.BoutiqueId);
        if (boutique is null)
        {
            // This will never happen as the boutique should exist
            return;
        }
    
        // Retrieve the user (boutique owner) by their ID
        var user = await _userRepository.GetByIdAsync(boutique.OwnerId ?? 0);
        if (user is null)
        {
            // Log the error if the user is not found
            _logger.LogError("User with ID {OwnerId} not found for boutique {BoutiqueId}", boutique.OwnerId, boutique.Id);
            return;
        }

        // Send an email notification to the boutique owner
        await _emailService.SendEmailAsync(user.Email, "Inventory Item Removed", $"Your inventory item {notification.Item.Caption} has been successfully removed");
    }
}       
