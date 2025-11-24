using System;
using System.Collections.Generic;
using Fase05.RepositoryInMemory.Domain;
using Fase05.RepositoryInMemory.Repository;

namespace Fase05.RepositoryInMemory.Services;

/// <summary>
/// Serviço de domínio que fala apenas com IRepository<CurrencyRate,int>
/// (não acessa coleções diretamente).
/// Contém validações simples (ex.: From/To não vazio, Rate > 0).
/// </summary>
public sealed class CurrencyRateService
{
    private readonly IRepository<CurrencyRate, int> _repo;

    public CurrencyRateService(IRepository<CurrencyRate, int> repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public CurrencyRate Register(CurrencyRate rate)
    {
        Validate(rate);
        return _repo.Add(rate);
    }

    public IReadOnlyList<CurrencyRate> ListAll() => _repo.ListAll();

    public CurrencyRate? GetById(int id) => _repo.GetById(id);

    public bool Update(CurrencyRate rate)
    {
        Validate(rate);
        return _repo.Update(rate);
    }

    public bool Remove(int id) => _repo.Remove(id);

    private static void Validate(CurrencyRate r)
    {
        if (r == null) throw new ArgumentNullException(nameof(r));
        if (string.IsNullOrWhiteSpace(r.From)) throw new ArgumentException("From inválido.");
        if (string.IsNullOrWhiteSpace(r.To)) throw new ArgumentException("To inválido.");
        if (r.Rate <= 0) throw new ArgumentException("Rate deve ser > 0.");
    }
}
