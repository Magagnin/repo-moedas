using System.Collections.Generic;

public interface IRateProvider
{
    decimal GetRate(string code, string policy);
    decimal? GetDirectRate(string from, string to);
    void SetCustomRate(string code, decimal rate);
    void SetDirectRate(string from, string to, decimal factor);
    IReadOnlyDictionary<string, decimal> GetBaseRates();
    IReadOnlyDictionary<string, decimal> GetCustomRates();
}

