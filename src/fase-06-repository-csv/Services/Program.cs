using System;
using System.IO;
using Fase06.RepositoryCsv.Domain;
using Fase06.RepositoryCsv.Repository;
using Fase06.RepositoryCsv.Services;

Console.WriteLine("Fase 6 — Repository CSV (CurrencyRate domain)");

// local do arquivo: coloca no diretório base do app
var path = Path.Combine(AppContext.BaseDirectory, "currency_rates.csv");
Console.WriteLine($"Arquivo CSV: {path}");
IRepository<CurrencyRate, int> repo = new CsvCurrencyRateRepository(path);
var service = new CurrencyRateService(repo);

while (true)
{
    Console.WriteLine("\nMenu Fase 6 (CSV):");
    Console.WriteLine("1) Registrar taxa");
    Console.WriteLine("2) Listar todas");
    Console.WriteLine("3) Buscar por Id");
    Console.WriteLine("4) Atualizar");
    Console.WriteLine("5) Remover");
    Console.WriteLine("0) Sair");
    Console.Write("Escolha: ");
    var opt = Console.ReadLine();
    if (opt == "0" || opt == null) break;

    try
    {
        switch (opt)
        {
            case "1": Register(service); break;
            case "2": ListAll(service); break;
            case "3": GetById(service); break;
            case "4": Update(service); break;
            case "5": Remove(service); break;
            default: Console.WriteLine("Opção inválida."); break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

static void Register(CurrencyRateService service)
{
    Console.Write("Id (inteiro): ");
    int id = int.Parse(Console.ReadLine() ?? "0");
    Console.Write("From (ex: USD): ");
    string from = (Console.ReadLine() ?? "").ToUpperInvariant();
    Console.Write("To (ex: BRL): ");
    string to = (Console.ReadLine() ?? "").ToUpperInvariant();
    Console.Write("Rate (decimal): ");
    decimal rate = decimal.Parse(Console.ReadLine() ?? "0", System.Globalization.CultureInfo.InvariantCulture);

    var cr = new CurrencyRate(id, from, to, rate);
    service.Register(cr);
    Console.WriteLine("Taxa registrada.");
}

static void ListAll(CurrencyRateService service)
{
    var all = service.ListAll();
    Console.WriteLine("Taxas cadastradas:");
    foreach (var r in all) Console.WriteLine($"#{r.Id}: {r.From} -> {r.To} = {r.Rate}");
}

static void GetById(CurrencyRateService service)
{
    Console.Write("Id: ");
    int id = int.Parse(Console.ReadLine() ?? "0");
    var r = service.GetById(id);
    if (r == null) Console.WriteLine("Não encontrado.");
    else Console.WriteLine($"#{r.Id}: {r.From} -> {r.To} = {r.Rate}");
}

static void Update(CurrencyRateService service)
{
    Console.Write("Id a atualizar: ");
    int id = int.Parse(Console.ReadLine() ?? "0");
    Console.Write("From (ex: USD): ");
    string from = (Console.ReadLine() ?? "").ToUpperInvariant();
    Console.Write("To (ex: BRL): ");
    string to = (Console.ReadLine() ?? "").ToUpperInvariant();
    Console.Write("Rate (decimal): ");
    decimal rate = decimal.Parse(Console.ReadLine() ?? "0", System.Globalization.CultureInfo.InvariantCulture);
    var updated = new CurrencyRate(id, from, to, rate);
    bool ok = service.Update(updated);
    Console.WriteLine(ok ? "Atualizado." : "Id não encontrado.");
}

static void Remove(CurrencyRateService service)
{
    Console.Write("Id a remover: ");
    int id = int.Parse(Console.ReadLine() ?? "0");
    bool ok = service.Remove(id);
    Console.WriteLine(ok ? "Removido." : "Id não encontrado.");
}

