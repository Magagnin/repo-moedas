using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    // Cotações base (1 unit of foreign = X BRL). Ex.: 1 USD = 5.20 BRL
    static readonly Dictionary<string, decimal> baseRates = new()
    {
        {"USD", 5.20m},
        {"EUR", 5.80m},
        {"GBP", 6.70m},
        {"JPY", 0.037m}
    };

    static void Main()
    {
        Console.WriteLine("Fase 2 — Conversor Procedural de Moedas (BRL <-> USD/EUR/GBP/JPY)");
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1) Converter BRL -> Estrangeira");
            Console.WriteLine("2) Converter Estrangeira -> BRL");
            Console.WriteLine("3) Converter Direto (Estrangeira -> Estrangeira)");
            Console.WriteLine("4) Atualizar cotação personalizada");
            Console.WriteLine("5) Mostrar cotações");
            Console.WriteLine("0) Sair");
            Console.Write("Escolha: ");
            var opt = Console.ReadLine();
            if (opt == "0" || opt == null) break;

            try
            {
                switch (opt)
                {
                    case "1":
                        DoBrlToForeign();
                        break;
                    case "2":
                        DoForeignToBrl();
                        break;
                    case "3":
                        DoForeignToForeign();
                        break;
                    case "4":
                        UpdateCustomRate();
                        break;
                    case "5":
                        ShowRates();
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

    static Dictionary<string, decimal> customRates = new();

    static decimal GetRate(string code, string policy)
    {
        // policy: FIXA, PERSONALIZADA, COMERCIAL, TURISMO
        if (policy == "PERSONALIZADA" && customRates.ContainsKey(code))
            return customRates[code];

        if (!baseRates.ContainsKey(code))
            throw new ArgumentException("Moeda não suportada");

        decimal rate = baseRates[code];

        return policy switch
        {
            "COMERCIAL" => rate * 0.995m, // menor spread
            "TURISMO" => rate * 1.02m,   // maior spread
            _ => rate // FIXA ou default
        };
    }

    static void DoBrlToForeign()
    {
        Console.Write("Valor em BRL: ");
        decimal brl = decimal.Parse(Console.ReadLine() ?? "0", CultureInfo.InvariantCulture);

        Console.Write("Código da moeda destino (USD/EUR/GBP/JPY): ");
        string to = (Console.ReadLine() ?? "").ToUpperInvariant();

        Console.Write("Política (FIXA/PERSONALIZADA/COMERCIAL/TURISMO): ");
        string policy = (Console.ReadLine() ?? "FIXA").ToUpperInvariant();

        decimal rate = GetRate(to, policy);
        decimal converted = brl / rate;
        Console.WriteLine($"Cotação usada: 1 {to} = {rate} BRL");
        Console.WriteLine($"Resultado: {Math.Round(converted, 4)} {to}");
    }

    static void DoForeignToBrl()
    {
        Console.Write("Valor em moeda estrangeria: ");
        decimal val = decimal.Parse(Console.ReadLine() ?? "0", CultureInfo.InvariantCulture);

        Console.Write("Código da moeda origem (USD/EUR/GBP/JPY): ");
        string from = (Console.ReadLine() ?? "").ToUpperInvariant();

        Console.Write("Política (FIXA/PERSONALIZADA/COMERCIAL/TURISMO): ");
        string policy = (Console.ReadLine() ?? "FIXA").ToUpperInvariant();

        decimal rate = GetRate(from, policy);
        decimal converted = val * rate;
        Console.WriteLine($"Cotação usada: 1 {from} = {rate} BRL");
        Console.WriteLine($"Resultado: {Math.Round(converted, 2)} BRL");
    }

    static void DoForeignToForeign()
    {
        Console.Write("Valor em moeda origem: ");
        decimal val = decimal.Parse(Console.ReadLine() ?? "0", CultureInfo.InvariantCulture);

        Console.Write("Código moeda origem (USD/EUR/GBP/JPY): ");
        string from = (Console.ReadLine() ?? "").ToUpperInvariant();

        Console.Write("Código moeda destino (USD/EUR/GBP/JPY): ");
        string to = (Console.ReadLine() ?? "").ToUpperInvariant();

        Console.Write("Política (FIXA/PERSONALIZADA/COMERCIAL/TURISMO): ");
        string policy = (Console.ReadLine() ?? "FIXA").ToUpperInvariant();

        // tentativa de cotação direta não disponível aqui: converter via BRL
        decimal rateFrom = GetRate(from, policy);
        decimal rateTo = GetRate(to, policy);

        // val (from) -> BRL -> to
        decimal brl = val * rateFrom;
        decimal converted = brl / rateTo;
        Console.WriteLine($"Cotação usadas: 1 {from} = {rateFrom} BRL ; 1 {to} = {rateTo} BRL");
        Console.WriteLine($"Resultado: {Math.Round(converted, 4)} {to}");
    }

    static void UpdateCustomRate()
    {
        Console.Write("Código moeda para atualizar (USD/EUR/GBP/JPY): ");
        string code = (Console.ReadLine() ?? "").ToUpperInvariant();
        Console.Write("Nova cotação (1 unit = X BRL): ");
        decimal newRate = decimal.Parse(Console.ReadLine() ?? "0", CultureInfo.InvariantCulture);
        customRates[code] = newRate;
        Console.WriteLine("Cotação atualizada (PERSONALIZADA).");
    }

    static void ShowRates()
    {
        Console.WriteLine("Cotações base:");
        foreach (var kv in baseRates)
            Console.WriteLine($"{kv.Key} = {kv.Value} BRL");

        if (customRates.Count > 0)
        {
            Console.WriteLine("Cotações customizadas:");
            foreach (var kv in customRates)
                Console.WriteLine($"{kv.Key} = {kv.Value} BRL (PERSONALIZADA)");
        }
    }
}

