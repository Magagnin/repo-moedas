# Fase 3 — Conversor OO sem interface

## Estrutura
- `MoneyConverterBase.cs` — base abstrata com fluxo comum.
- `BrlToForeignConverter.cs` — BRL -> estrangeira.
- `ForeignToBrlConverter.cs` — estrangeira -> BRL.
- `DirectForeignConverter.cs` — tentativa de cotação direta; se não disponível, converte via BRL.
- `RateProvider.cs` — fornecedor de cotações (base, personalizado e diretas).
- `Formatter.cs` — utilitário para arredondamento/formatação.
- `Program.cs` — menu de interação.

## Como executar
dotnet run dentro da pasta do projeto.

## Observações
- O ponto de composição (criação dos conversores) está no cliente (`Program.cs`), que conhece as classes concretas — isso é intencional nesta fase (sem interfaces).
- Na próxima fase (Fase 4) faríamos `IMoneyConverter` e injeção/fábrica para remover esse acoplamento.

