using SoftCollection.SharedKernel.Time;

namespace SoftCollection.SharedKernel.Tests;

public class BusinessClockTests
{
    [Fact]
    public void Today_is_computed_in_Bishkek_time()
    {
        // 20:30 UTC 31.10 — в Бишкеке (UTC+6) уже 02:30 01.11.
        var clock = new BusinessClock(new FixedTimeProvider(new DateTimeOffset(2026, 10, 31, 20, 30, 0, TimeSpan.Zero)));

        Assert.Equal(new DateOnly(2026, 11, 1), clock.Today);
        Assert.Equal(TimeSpan.FromHours(6), clock.LocalNow.Offset);
    }

    private sealed class FixedTimeProvider(DateTimeOffset now) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => now;
    }
}
