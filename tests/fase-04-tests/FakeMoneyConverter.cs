public sealed class FakeMoneyConverter : IMoneyConverter
{
    public decimal ReturnedValue { get; set; } = 123.45m;
    public decimal LastAmount { get; private set; }
    public string LastFrom { get; private set; } = string.Empty;
    public string LastTo { get; private set; } = string.Empty;
    public string LastPolicy { get; private set; } = string.Empty;

    public decimal Convert(decimal amount, string from, string to, string policy)
    {
        LastAmount = amount;
        LastFrom = from;
        LastTo = to;
        LastPolicy = policy;
        return ReturnedValue;
    }
}

