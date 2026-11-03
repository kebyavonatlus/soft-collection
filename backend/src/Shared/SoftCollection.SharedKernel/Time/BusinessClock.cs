namespace SoftCollection.SharedKernel.Time;

public sealed class BusinessClock(TimeProvider timeProvider, TimeZoneInfo timeZone) : IBusinessClock
{
    public const string DefaultTimeZoneId = "Asia/Bishkek";

    public BusinessClock(TimeProvider timeProvider)
        : this(timeProvider, TimeZoneInfo.FindSystemTimeZoneById(DefaultTimeZoneId))
    {
    }

    public TimeZoneInfo TimeZone { get; } = timeZone;

    public DateTimeOffset UtcNow => timeProvider.GetUtcNow();

    public DateTimeOffset LocalNow => TimeZoneInfo.ConvertTime(UtcNow, TimeZone);

    public DateOnly Today => DateOnly.FromDateTime(LocalNow.DateTime);
}
