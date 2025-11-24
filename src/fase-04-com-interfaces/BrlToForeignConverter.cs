public sealed class BrlToForeignConverter : IMoneyConverter
{
    private readonly IRateProvider _rates;
    public BrlToForeignConverter(IRateProvider rates) => _rates = rates;

    public decimal Convert(decimal amount, string from, string to, string policy)
    {
        if (!string.Equals(from, "BRL", System.StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("BrlToForeignConverter: origem deve ser BRL");

        decimal rate = _rates.GetRate(to, policy);
        return amount / rate;
    }
}

