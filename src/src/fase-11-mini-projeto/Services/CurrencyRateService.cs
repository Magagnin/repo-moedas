using System;
using System.Collections.Generic;
using Fase11.MiniProject.Domain;
using Fase11.MiniProject.Repository;

namespace Fase11.MiniProject.Services;

public sealed class CurrencyRateService
{
    private readonly IReadRepository<CurrencyRate,int> _read;
    private readonly IWriteRepository<CurrencyRate,int> _write;

    public CurrencyRateService(IReadRepository<CurrencyRate,int> read, IWriteRepository<CurrencyRate,int> write)
        => (_read, _write) = (read ?? throw new ArgumentNullException(nameof(read)), write ?? throw new ArgumentNullException(nameof(write)));

    public CurrencyRate Register(CurrencyRate r)
    {
        Validate(r);
        return _write.Add(r);
    }

    public IReadOnlyList<CurrencyRate> All() => _read.ListAll();

    public CurrencyRate? Find(int id) => _read.GetById(id);

    public bool Rename(int id, string newFrom, string newTo)
    {
        var cur = _read.GetById(id);
        if (cur is null) return false;
        var updated = cur with { From = newFrom, To = newTo };
        return _write.Update(updated);
    }

    public bool Remove(int id) => _write.Remove(id);

    private static void Validate(CurrencyRate r)
    {
        if (r == null) throw new ArgumentNullException(nameof(r));
        if (string.IsNullOrWhiteSpace(r.From)) throw new ArgumentException("From invalid");
        if (string.IsNullOrWhiteSpace(r.To)) throw new ArgumentException("To invalid");
        if (r.Rate <= 0) throw new ArgumentException("Rate must be > 0");
    }
}
