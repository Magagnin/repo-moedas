using System;

public abstract class MoneyConverterBase
{
    // Passos comuns: validar inputs e aplicar arredondamento/format.
    public string Convert(decimal amount, string from, string to, string policy, RateProvider rates, int decimals = 4)
    {
        if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
            throw new ArgumentException("Moedas inválidas.");

        decimal result = ApplyConversion(amount, from.ToUpperInvariant(), to.ToUpperInvariant(), policy.ToUpperInvariant(), rates);

        // formatação final
        return Formatter.FormatDecimal(result, decimals) + $" {to.ToUpperInvariant()}";
    }

    protected abstract decimal ApplyConversion(decimal amount, string from, string to, string policy, RateProvider rates);
}

