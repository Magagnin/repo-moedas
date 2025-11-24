using System;
using System.Collections.Generic;

public class RateProvider
{
    // base rates: 1 unit of foreign currency = X BRL.
    private readonly Dictionary<string, decimal> baseRates = new()
    {
        {"USD", 5.20m},
        {"EUR", 5.80m},
        {"GBP", 6.70m},
        {"JPY", 0.037m}
    };

    // custom overrides
    private readonly Dictionary<string, decimal> customRates = new();

    // direct rates (1 FROM = X TO)
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

    public void SetCustomRate(string code, decimal rate)
    {
        customRates[code.ToUpperInvariant()] = rate;
    }

    public void SetDirectRate(string from, string to, decimal factor)
    {
        directRates[(from.ToUpperInvariant(), to.ToUpperInvariant())] = factor;
    }

    public decimal? GetDirectRate(string from, string to)
    {
        if (directRates.TryGetValue((from.ToUpperInvariant(), to.ToUpperInvariant()), out var f))
            return f;
        return null;
    }

    public IReadOnlyDictionary<string, decimal> GetBaseRates() => baseRates;
    public IReadOnlyDictionary<string, decimal> GetCustomRates() => customRates;
}

