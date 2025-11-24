public sealed class BrlToForeignConverter : MoneyConverterBase
{
    protected override decimal ApplyConversion(decimal amount, string from, string to, string policy, RateProvider rates)
    {
        // from is BRL here, to is foreign
        if (from != "BRL") throw new ArgumentException("BrlToForeignConverter: origem deve ser BRL");
        decimal rate = rates.GetRate(to, policy);
        return amount / rate;
    }
}

