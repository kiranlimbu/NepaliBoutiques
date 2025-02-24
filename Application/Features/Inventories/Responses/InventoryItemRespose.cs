namespace Application.Features.Inventories.Responses;

public sealed class InventoryItemResponse
{
    public int Id { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
    public string Caption { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; }
}
