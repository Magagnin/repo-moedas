# Fase 4 — Interface plugável e testável (Conversão de Moedas)

## Objetivo
Evoluir a Fase 3 introduzindo:
- Contrato explícito (`IMoneyConverter`) para o passo variável (conversão).
- Contrato para `IRateProvider` para tornar trocável o provedor de taxas.
- Ponto único de composição (MoneyConverterCatalog).
- Cliente (`ConversionService`) dependendo apenas do contrato.
- Teste com dublê (FakeMoneyConverter) para validar o cliente sem I/O.

## Como está organizado
- `IMoneyConverter.cs` — contrato do conversor.
- `IRateProvider.cs` — contrato do provedor de cotações.
- `DefaultRateProvider.cs` — implementação real de cotações.
- Concretos que implementam `IMoneyConverter`: `BrlToForeignConverter`, `ForeignToBrlConverter`, `DirectForeignConverter`.
- `MoneyConverterCatalog` — fábrica/ catálogo (ponto único de composição).
- `ConversionService` — cliente que recebe `IMoneyConverter` (injeção).
- `Program.cs` — demonstração: composição no início (catalog → converter) e serviço usando apenas a interface.

## Como executar
1. Abra a pasta `src/fase-04-com-interfaces`
2. `dotnet run`

## Como rodar os testes
1. Vá para `tests/fase-04-tests`
2. `dotnet test`

O teste demonstra uso de um dublê (`FakeMoneyConverter`) injetado em `ConversionService`. Não há I/O no teste.
