# Fase 5 — Repository InMemory (Conversão de Moedas)

## Domínio
`CurrencyRate` — representa uma taxa entre duas moedas cadastrada pelo usuário.

## Política de Id
**Id informado pelo usuário** (opção A do cliente). O repositório apenas usa a função `getId` para extrair o Id.

## Contrato do Repository
`IRepository<T, TId>` com operações:
- Add, GetById, ListAll, Update, Remove

## Implementação
`InMemoryRepository<T,TId>` usa `Dictionary<TId, T>`. Não há I/O.

## Serviço
`CurrencyRateService` fala apenas com `IRepository<CurrencyRate,int>` e contém validações de domínio.

## Como executar
```bash
cd src/fase-05-repository-inmemory
dotnet run
