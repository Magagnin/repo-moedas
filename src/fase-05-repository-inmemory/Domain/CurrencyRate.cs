namespace Fase05.RepositoryInMemory.Domain;

public sealed record CurrencyRate(int Id, string From, string To, decimal Rate);
