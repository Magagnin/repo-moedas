using System;
using System.Threading;
using Fase09.DublesAsync.Contracts;
using Fase09.DublesAsync.Services;
using Fase09.DublesAsync.Dubles;

Console.WriteLine("Fase 9 — Dublês avançados e testes assíncronos");

var reader = new FakeReader<int>(new[] { 1, 2, 3 });
var writer = new FakeWriter<int>(failTimes: 1);
var clock = new FakeClock();

var service = new PumpService<int>(reader, writer, clock);

var result = await service.RunAsync();
Console.WriteLine($"Itens processados: {result}");
