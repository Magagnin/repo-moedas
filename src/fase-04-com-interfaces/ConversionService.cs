using System;

public sealed class ConversionService
{
    private readonly IMoneyConverter _converter;

    public ConversionService(IMoneyConverter converter)
    {
        _converter = converter ?? throw new ArgumentNullException(nameof(converter));
    }

    /// <summary>
    /// Usa o IMoneyConverter injetado para converter e retorna string formatada
    /// com o sufixo da moeda destino.
    /// </summary>
    public string ConvertAndFormat(decimal amount, string from, string to, string policy, int decimals = 4)
    {
        decimal raw = _converter.Convert(amount, from, to, policy);
        return Formatter.FormatDecimal(raw, decimals) + $" {to.ToUpperInvariant()}";
    }
}

