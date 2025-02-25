namespace Core.Abstractions;

public abstract class BaseEntity : IAuditable
{
    
    private readonly List<ICoreEvent> _coreEvents = [];


    protected BaseEntity(int id)
    {
        Id = id;
    }

    // represents the unique identifier for the entity
    public int Id { get; init; }

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
    public IReadOnlyList<ICoreEvent> GetCoreEvents() => _coreEvents.ToList();

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