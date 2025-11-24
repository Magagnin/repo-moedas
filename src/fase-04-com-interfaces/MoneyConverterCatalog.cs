using System;

public static class MoneyConverterCatalog
{
    /// <summary>
    /// Resolve um IMoneyConverter para um determinado modo de conversão.
    /// mode values (examples):
    ///   "BRL_TO_FOREIGN", "FOREIGN_TO_BRL", "DIRECT"
    /// </summary>
    public static IMoneyConverter Resolve(string mode, IRateProvider rates)
    {
        return mode?.ToUpperInvariant() switch
        {
            "BRL_TO_FOREIGN" => new BrlToForeignConverter(rates),
            "FOREIGN_TO_BRL" => new ForeignToBrlConverter(rates),
            "DIRECT" => new DirectForeignConverter(rates),
            _ => throw new ArgumentException($"Modo desconhecido: {mode}")
        };
    }
}

