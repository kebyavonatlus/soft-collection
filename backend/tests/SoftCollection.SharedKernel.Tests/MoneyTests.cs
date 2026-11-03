using SoftCollection.SharedKernel.Finance;

namespace SoftCollection.SharedKernel.Tests;

public class MoneyTests
{
    [Fact]
    public void Adds_amounts_in_same_currency()
    {
        var sum = new Money(1700m, Currency.Kgs) + new Money(900m, Currency.Kgs);

        Assert.Equal(new Money(2600m, Currency.Kgs), sum);
    }

    [Fact]
    public void Refuses_to_add_som_and_dollars()
    {
        var som = new Money(100m, Currency.Kgs);
        var usd = new Money(100m, Currency.Usd);

        Assert.Throws<CurrencyMismatchException>(() => { _ = som + usd; });
    }

    [Fact]
    public void Rounds_to_two_decimals()
    {
        Assert.Equal(10.13m, new Money(10.125m, Currency.Kgs).Amount);
    }

    [Theory]
    [InlineData(12400, "12 400,00")]
    [InlineData(3100.5, "3 100,50")]
    [InlineData(0, "0,00")]
    [InlineData(1234567.89, "1 234 567,89")]
    public void Formats_for_display_like_in_the_spec(decimal amount, string expected)
    {
        Assert.Equal(expected, new Money(amount, Currency.Kgs).ToDisplayString());
    }
}
