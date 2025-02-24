using Application.Abstractions;


namespace Infrastructure.ExternalServices;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

