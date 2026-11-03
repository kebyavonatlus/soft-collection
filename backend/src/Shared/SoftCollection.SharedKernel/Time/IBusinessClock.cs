namespace SoftCollection.SharedKernel.Time;

/// <summary>
/// Время организации. Все бизнес-правила (часы звонков, конец дня обещания,
/// ночной пересчёт) считаются в часовом поясе организации, по умолчанию Asia/Bishkek.
/// </summary>
public interface IBusinessClock
{
    TimeZoneInfo TimeZone { get; }

    DateTimeOffset UtcNow { get; }

    /// <summary>Текущее время в часовом поясе организации.</summary>
    DateTimeOffset LocalNow { get; }

    /// <summary>Текущая дата в часовом поясе организации.</summary>
    DateOnly Today { get; }
}
