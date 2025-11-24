public sealed class ForeignToBrlConverter : MoneyConverterBase
{
    protected override decimal ApplyConversion(decimal amount, string from, string to, string policy, RateProvider rates)
    {
        // to is BRL here, from is foreign
        if (to != "BRL") throw new ArgumentException("ForeignToBrlConverter: destino deve ser BRL");
        decimal rate = rates.GetRate(from, policy);
        return amount * rate;
    }
}

