using System;
using System.IO;
using Fase11.MiniProject.Domain;
using Fase11.MiniProject.Repository;
using Fase11.MiniProject.Services;

Console.WriteLine("Fase 11 — Mini-projeto de consolidação (CurrencyRate)");
var path = Path.Combine(AppContext.BaseDirectory, "currency_rates_fase11.json");
IReadRepository<CurrencyRate,int> readRepo = new JsonCurrencyRateRepository(path);
IWriteRepository<CurrencyRate,int> writeRepo = (IWriteRepository<CurrencyRate,int>)readRepo;
var service = new CurrencyRateService(readRepo, writeRepo);

// demo: add a few, list, rename, remove
service.Register(new CurrencyRate(1, "USD", "BRL", 5.2m));
service.Register(new CurrencyRate(2, "EUR", "BRL", 6.1m));
Console.WriteLine("All rates:");
foreach (var r in service.All()) Console.WriteLine($"#{r.Id}: {r.From}->{r.To} = {r.Rate}");
Console.WriteLine("Rename id 2 EUR->USD");
service.Rename(2, "EUR", "USD");
Console.WriteLine("After rename:");
foreach (var r in service.All()) Console.WriteLine($"#{r.Id}: {r.From}->{r.To} = {r.Rate}");
