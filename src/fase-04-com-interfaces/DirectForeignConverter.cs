public sealed class DirectForeignConverter : IMoneyConverter
{
    private readonly IRateProvider _rates;
    public DirectForeignConverter(IRateProvider rates) => _rates = rates;

    public decimal Convert(decimal amount, string from, string to, string policy)
    {
        if (string.Equals(from, to, System.StringComparison.OrdinalIgnoreCase))
            return amount;

        decimal? direct = _rates.GetDirectRate(from, to);
        if (direct.HasValue)
        {
            // definimos direct como "1 FROM = X TO" => amount * X = amount in TO
            return amount * direct.Value;
        }

        // fallback via BRL
        decimal fromToBrl = _rates.GetRate(from, policy);
        decimal toToBrl = _rates.GetRate(to, policy);
        decimal brl = amount * fromToBrl;
        return brl / toToBrl;
    }
}

