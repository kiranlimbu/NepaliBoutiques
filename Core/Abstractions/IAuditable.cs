namespace Core.Abstractions;

public interface IAuditable
{
    string CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }
    string LastModifiedBy { get; set; }
    DateTime LastModifiedAt { get; set; }
}

