using Core.Abstractions;
using Core.Entities;

namespace Core.Events;

public sealed record UserCreatedCoreEvent(User User) : ICoreEvent;

public sealed record UserUpdatedCoreEvent(User User) : ICoreEvent;

public sealed record UserDeletedCoreEvent(User User) : ICoreEvent;

public sealed record UserLoggedInCoreEvent(User User) : ICoreEvent;

public sealed record UserLoggedOutCoreEvent(User User) : ICoreEvent;

public sealed record UserPasswordResetCoreEvent(User User) : ICoreEvent;

public sealed record UserPasswordResetRequestedCoreEvent(User User) : ICoreEvent;


