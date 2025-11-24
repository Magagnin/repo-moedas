using System.Globalization;

public static class Formatter
{
    public static string FormatDecimal(decimal value, int decimals)
    {
        // arredonda e retorna string com padrão invariante
        decimal rounded = Math.Round(value, decimals);
        return rounded.ToString($"F{decimals}", CultureInfo.InvariantCulture).TrimEnd('0').TrimEnd('.');
    }
}

