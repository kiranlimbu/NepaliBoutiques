namespace Core.Abstractions;

public abstract class Entity
{
    private readonly List<ICoreEvent> _coreEvents = [];

    // todo: add public UpdateAuditableEntity(CreatedBy, CreatedAt, LastModifiedBy, LastModifiedAt)

    /// <summary>
    /// Gets the list of core events associated with this entity.
    /// </summary>
    public IReadOnlyList<ICoreEvent> CoreEvents => _coreEvents.AsReadOnly();

    /// <summary>
    /// Raises a core event to the entity.
    /// </summary>
    /// <param name="coreEvent">The core event to raise.</param>
    protected void RaiseCoreEvent(ICoreEvent coreEvent)
    {
        _coreEvents.Add(coreEvent);
    }

    /// <summary>
    /// Clears all core events from the entity.
    /// </summary>
    public void ClearCoreEvents()
    {
        _coreEvents.Clear();
    }
}