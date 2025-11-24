public sealed class DirectForeignConverter : MoneyConverterBase
{
    protected override decimal ApplyConversion(decimal amount, string from, string to, string policy, RateProvider rates)
    {
        // Preferir cotação direta; se não, via BRL
        if (from == to) return amount;

        decimal? direct = rates.GetDirectRate(from, to);
        if (direct.HasValue)
        {
            // direct: 1 to = X from? Define: direct rate = amount_in_from -> converted = amount * direct
            // we define direct as 1 TO = X FROM, so conversion factor = 1 / direct? To avoid confusion, we store direct as 1 FROM = X TO
            // We'll store direct as 1 FROM = X TO so direct * amount => amount in TO
            return amount * direct.Value;
        }

        // via BRL
        decimal fromToBrl = rates.GetRate(from, policy); // 1 FROM = fromToBrl BRL
        decimal toToBrl = rates.GetRate(to, policy);     // 1 TO = toToBrl BRL
        decimal brl = amount * fromToBrl;
        return brl / toToBrl;
    }
}

