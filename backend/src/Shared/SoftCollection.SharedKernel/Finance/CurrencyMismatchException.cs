namespace SoftCollection.SharedKernel.Finance;

public sealed class CurrencyMismatchException : InvalidOperationException
{
    public CurrencyMismatchException()
    {
    }

    public CurrencyMismatchException(string message)
        : base(message)
    {
    }

    public CurrencyMismatchException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public CurrencyMismatchException(Currency left, Currency right)
        : base($"Нельзя складывать или сравнивать суммы в разных валютах: {left} и {right}.")
    {
    }
}
