namespace Core.Abstractions;

public abstract class BaseEntity : IAuditable
{
    private readonly List<ICoreEvent> _coreEvents = [];

    /// <summary>
    /// Gets or sets the user who created/last modified the entity.
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string LastModifiedBy { get; set; } = string.Empty;
    public DateTime LastModifiedAt { get; set; }

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