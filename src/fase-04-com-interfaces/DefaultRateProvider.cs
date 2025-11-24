using System;
using System.Collections.Generic;

public class DefaultRateProvider : IRateProvider
{
    private readonly Dictionary<string, decimal> baseRates = new()
    {
        {"USD", 5.20m},
        {"EUR", 5.80m},
        {"GBP", 6.70m},
        {"JPY", 0.037m}
    };

    private readonly Dictionary<string, decimal> customRates = new();
    private readonly Dictionary<(string from, string to), decimal> directRates = new();

    public decimal GetRate(string code, string policy)
    {
        code = code.ToUpperInvariant();
        policy = policy.ToUpperInvariant();

        if (policy == "PERSONALIZADA" && customRates.ContainsKey(code))
            return customRates[code];

        if (!baseRates.ContainsKey(code))
            throw new ArgumentException($"Moeda não suportada: {code}");

        decimal rate = baseRates[code];

        return policy switch
        {
            "COMERCIAL" => rate * 0.995m,
            "TURISMO" => rate * 1.02m,
            _ => rate
        };
    }

    public decimal? GetDirectRate(string from, string to)
    {
        if (directRates.TryGetValue((from.ToUpperInvariant(), to.ToUpperInvariant()), out var v))
            return v;
        return null;
    }

    public void SetCustomRate(string code, decimal rate) => customRates[code.ToUpperInvariant()] = rate;
    public void SetDirectRate(string from, string to, decimal factor) =>
        directRates[(from.ToUpperInvariant(), to.ToUpperInvariant())] = factor;

    public IReadOnlyDictionary<string, decimal> GetBaseRates() => baseRates;
    public IReadOnlyDictionary<string, decimal> GetCustomRates() => customRates;
}

