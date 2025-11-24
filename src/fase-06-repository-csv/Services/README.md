# Fase 6 — Repository CSV (Conversão de Moedas)

## Domínio
`CurrencyRate` — Id, From, To, Rate

## CSV format
- Encoding: UTF-8
- Header: `Id,From,To,Rate`
- Separator: comma `,`
- Quotes: `"`; internal quotes escaped as `""`
- Policy de Id: **Id informado pelo usuário** (opção A)

## Arquivos gerados
`currency_rates.csv` no diretório do aplicativo.

## Como executar
```bash
cd src/fase-06-repository-csv
dotnet run

