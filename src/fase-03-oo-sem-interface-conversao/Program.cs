using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Fase 3 — Conversor OO sem interface (MoneyConverterBase + concretos)");
        var rates = new RateProvider();

        // exemplo de direct rate (opcional)
        // rates.SetDirectRate("USD", "EUR", 0.90m); // 1 USD = 0.90 EUR (exemplo)

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1) BRL -> Estrangeira");
            Console.WriteLine("2) Estrangeira -> BRL");
            Console.WriteLine("3) Estrangeira -> Estrangeira (direta ou via BRL)");
            Console.WriteLine("4) Definir cotação personalizada (1 unit = X BRL)");
            Console.WriteLine("5) Definir cotação direta (1 FROM = X TO)");
            Console.WriteLine("6) Mostrar cotações");
            Console.WriteLine("0) Sair");
            Console.Write("Escolha: ");
            string? op = Console.ReadLine();
            if (op == "0" || op == null) break;

            try
            {
                switch (op)
                {
                    case "1":
                        RunBrlToForeign(rates);
                        break;
                    case "2":
                        RunForeignToBrl(rates);
                        break;
                    case "3":
                        RunForeignToForeign(rates);
                        break;
                    case "4":
                        SetCustomRate(rates);
                        break;
                    case "5":
                        SetDirectRate(rates);
                        break;
                    case "6":
                        ShowRates(rates);
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }

    static void RunBrlToForeign(RateProvider rates)
    {
        Console.Write("Valor em BRL: ");
        decimal brl = decimal.Parse(Console.ReadLine() ?? "0", System.Globalization.CultureInfo.InvariantCulture);

        Console.Write("Moeda destino (USD/EUR/GBP/JPY): ");
        string to = (Console.ReadLine() ?? "").ToUpperInvariant();

        Console.Write("Política (FIXA/PERSONALIZADA/COMERCIAL/TURISMO): ");
        string policy = (Console.ReadLine() ?? "FIXA").ToUpperInvariant();

        var conv = new BrlToForeignConverter();
        string outp = conv.Convert(brl, "BRL", to, policy, rates, 4);
        Console.WriteLine($"Resultado: {outp}");
    }

    static void RunForeignToBrl(RateProvider rates)
    {
        Console.Write("Valor em moeda estrangeira: ");
        decimal val = decimal.Parse(Console.ReadLine() ?? "0", System.Globalization.CultureInfo.InvariantCulture);

        Console.Write("Moeda origem (USD/EUR/GBP/JPY): ");
        string from = (Console.ReadLine() ?? "").ToUpperInvariant();

        Console.Write("Política (FIXA/PERSONALIZADA/COMERCIAL/TURISMO): ");
        string policy = (Console.ReadLine() ?? "FIXA").ToUpperInvariant();

        var conv = new ForeignToBrlConverter();
        string outp = conv.Convert(val, from, "BRL", policy, rates, 2);
        Console.WriteLine($"Resultado: {outp}");
    }

    static void RunForeignToForeign(RateProvider rates)
    {
        Console.Write("Valor em moeda origem: ");
        decimal val = decimal.Parse(Console.ReadLine() ?? "0", System.Globalization.CultureInfo.InvariantCulture);

        Console.Write("Moeda origem (USD/EUR/GBP/JPY): ");
        string from = (Console.ReadLine() ?? "").ToUpperInvariant();

        Console.Write("Moeda destino (USD/EUR/GBP/JPY): ");
        string to = (Console.ReadLine() ?? "").ToUpperInvariant();

        Console.Write("Política (FIXA/PERSONALIZADA/COMERCIAL/TURISMO): ");
        string policy = (Console.ReadLine() ?? "FIXA").ToUpperInvariant();

        var conv = new DirectForeignConverter();
        string outp = conv.Convert(val, from, to, policy, rates, 4);
        Console.WriteLine($"Resultado: {outp}");
    }

    static void SetCustomRate(RateProvider rates)
    {
        Console.Write("Moeda (USD/EUR/GBP/JPY): ");
        string code = (Console.ReadLine() ?? "").ToUpperInvariant();
        Console.Write("Nova cotação (1 unit = X BRL): ");
        decimal rate = decimal.Parse(Console.ReadLine() ?? "0", System.Globalization.CultureInfo.InvariantCulture);
        rates.SetCustomRate(code, rate);
        Console.WriteLine("Cotação personalizada definida.");
    }

    static void SetDirectRate(RateProvider rates)
    {
        Console.Write("FROM (ex: USD): ");
        string from = (Console.ReadLine() ?? "").ToUpperInvariant();
        Console.Write("TO (ex: EUR): ");
        string to = (Console.ReadLine() ?? "").ToUpperInvariant();
        Console.Write("Fator (1 FROM = X TO): ");
        decimal factor = decimal.Parse(Console.ReadLine() ?? "0", System.Globalization.CultureInfo.InvariantCulture);
        rates.SetDirectRate(from, to, factor);
        Console.WriteLine("Cotação direta definida.");
    }

    static void ShowRates(RateProvider rates)
    {
        Console.WriteLine("Base rates (1 unit = X BRL):");
        foreach (var kv in rates.GetBaseRates())
            Console.WriteLine($"{kv.Key} = {kv.Value} BRL");

        var customs = rates.GetCustomRates();
        if (customs.Count > 0)
        {
            Console.WriteLine("Custom rates:");
            foreach (var kv in customs)
                Console.WriteLine($"{kv.Key} = {kv.Value} BRL");
        }
    }
}

