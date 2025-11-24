using System.Globalization;

public static class Formatter
{
    public static string FormatDecimal(decimal value, int decimals)
    {
        decimal rounded = Math.Round(value, decimals);
        string formatted = rounded.ToString($"F{decimals}", CultureInfo.InvariantCulture);
        // remove zeros e ponto quando possível (como em Fase 3)
        return formatted.TrimEnd('0').TrimEnd('.');
    }
}

