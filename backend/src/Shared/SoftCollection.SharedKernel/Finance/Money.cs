using System.Globalization;

namespace SoftCollection.SharedKernel.Finance;

/// <summary>
/// Сумма с валютой. Хранится с точностью до 2 знаков (numeric(18,2) в БД).
/// Арифметика между разными валютами запрещена.
/// </summary>
public readonly record struct Money
{
    private static readonly NumberFormatInfo DisplayFormat = new()
    {
        NumberGroupSeparator = " ",
        NumberDecimalSeparator = ",",
        NumberGroupSizes = [3],
        NegativeSign = "-",
    };

    public Money(decimal amount, Currency currency)
    {
        Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency;
    }

    public decimal Amount { get; }

    public Currency Currency { get; }

    public static Money Zero(Currency currency) => new(0m, currency);

    public bool IsZero => Amount == 0m;

    public bool IsPositive => Amount > 0m;

    public static Money operator +(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static bool operator >(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount > right.Amount;
    }

    public static bool operator <(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount < right.Amount;
    }

    public static bool operator >=(Money left, Money right) => !(left < right);

    public static bool operator <=(Money left, Money right) => !(left > right);

    public Money Add(Money other) => this + other;

    public Money Subtract(Money other) => this - other;

    /// <summary>
    /// Формат интерфейса: пробел между тысячами и два знака после запятой — «12 400,00» (ТЗ, раздел 5).
    /// </summary>
    public string ToDisplayString() => Amount.ToString("#,0.00", DisplayFormat);

    public override string ToString() => $"{ToDisplayString()} {Currency.ToString().ToUpperInvariant()}";

    private static void EnsureSameCurrency(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new CurrencyMismatchException(left.Currency, right.Currency);
        }
    }
}
