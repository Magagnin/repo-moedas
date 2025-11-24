public sealed class ForeignToBrlConverter : IMoneyConverter
{
    private readonly IRateProvider _rates;
    public ForeignToBrlConverter(IRateProvider rates) => _rates = rates;

    public decimal Convert(decimal amount, string from, string to, string policy)
    {
        if (!string.Equals(to, "BRL", System.StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("ForeignToBrlConverter: destino deve ser BRL");

        decimal rate = _rates.GetRate(from, policy);
        return amount * rate;
    }
}

