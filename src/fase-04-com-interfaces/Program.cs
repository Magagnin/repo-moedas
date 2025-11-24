using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Fase 4 — Interface plugável e testável (Conversão de Moedas)");

        var rates = new DefaultRateProvider();

        // exemplo: definir uma cotação direta opcional
        // rates.SetDirectRate("USD", "EUR", 0.90m);

        Console.Write("Escolha o modo (BRL_TO_FOREIGN / FOREIGN_TO_BRL / DIRECT): ");
        var mode = (Console.ReadLine() ?? "BRL_TO_FOREIGN").ToUpperInvariant();

        // ponto único de composição: resolve o converter concreto a partir do modo
        IMoneyConverter converter = MoneyConverterCatalog.Resolve(mode, rates);

        // cliente depende apenas da interface:
        var service = new ConversionService(converter);

        Console.Write("Valor: ");
        decimal amount = decimal.Parse(Console.ReadLine() ?? "0", System.Globalization.CultureInfo.InvariantCulture);

        Console.Write("From (ex: BRL, USD, EUR): ");
        string from = (Console.ReadLine() ?? "BRL").ToUpperInvariant();

        Console.Write("To (ex: USD, EUR, BRL): ");
        string to = (Console.ReadLine() ?? "USD").ToUpperInvariant();

        Console.Write("Política (FIXA/PERSONALIZADA/COMERCIAL/TURISMO): ");
        string policy = (Console.ReadLine() ?? "FIXA").ToUpperInvariant();

        var result = service.ConvertAndFormat(amount, from, to, policy, 4);
        Console.WriteLine($"\nResultado: {result}");
        Console.WriteLine("\n(Observação: Program.cs compõe em um único ponto usando MoneyConverterCatalog; ConversionService não conhece concretos.)");
    }
}

