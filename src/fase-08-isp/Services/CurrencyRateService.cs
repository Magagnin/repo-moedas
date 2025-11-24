using System;
using System.Collections.Generic;
using Fase08.Isp.Domain;
using Fase08.Isp.Repository;

namespace Fase08.Isp.Services;

/// <summary>
/// Serviço de domínio refatorado para depender de IReadRepository e IWriteRepository (ISP).
/// </summary>
public sealed class CurrencyRateService
{
    private readonly IReadRepository<CurrencyRate, int> _readRepo;
    private readonly IWriteRepository<CurrencyRate, int> _writeRepo;

    // Depende dos contratos mínimos (ISP)
    public CurrencyRateService(
        IReadRepository<CurrencyRate, int> readRepo, 
        IWriteRepository<CurrencyRate, int> writeRepo)
    {
        _readRepo = readRepo ?? throw new ArgumentNullException(nameof(readRepo));
        _writeRepo = writeRepo ?? throw new ArgumentNullException(nameof(writeRepo));
    }

    public CurrencyRate Register(CurrencyRate rate)
    {
        Validate(rate);
        return _writeRepo.Add(rate); // usa IWrite
    }

    public IReadOnlyList<CurrencyRate> ListAll() => _readRepo.ListAll(); // usa IRead

    public CurrencyRate? GetById(int id) => _readRepo.GetById(id); // usa IRead

    public bool Update(CurrencyRate rate)
    {
        Validate(rate);
        return _writeRepo.Update(rate); // usa IWrite
    }

    public bool Remove(int id) => _writeRepo.Remove(id); // usa IWrite

    private static void Validate(CurrencyRate r)
    {
        if (r == null) throw new ArgumentNullException(nameof(r));
        if (string.IsNullOrWhiteSpace(r.From)) throw new ArgumentException("From inválido.");
        if (string.IsNullOrWhiteSpace(r.To)) throw new ArgumentException("To inválido.");
        if (r.Rate <= 0) throw new ArgumentException("Rate deve ser > 0.");
    }
}
