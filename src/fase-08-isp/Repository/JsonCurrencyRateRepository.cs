using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Fase08.Isp.Domain;

namespace Fase08.Isp.Repository;

/// <summary>
/// Repository JSON para CurrencyRate.
/// Implementa IReadRepository e IWriteRepository (ISP).
/// </summary>
public sealed class JsonCurrencyRateRepository : 
    IReadRepository<CurrencyRate, int>, 
    IWriteRepository<CurrencyRate, int>
{
    private readonly string _path;
    private static readonly JsonSerializerOptions _opts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
#if NET6_0_OR_GREATER
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
#else
        IgnoreNullValues = true,
#endif
        WriteIndented = true
    };

    public JsonCurrencyRateRepository(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Path inválido", nameof(path));
        _path = path;
    }

    // ---------- IWriteRepository Implementação ----------
    public CurrencyRate Add(CurrencyRate entity)
    {
        var list = Load();
        // se já existir id, substitui
        list.RemoveAll(r => r.Id == entity.Id);
        list.Add(entity);
        Save(list);
        return entity;
    }

    public bool Update(CurrencyRate entity)
    {
        var list = Load();
        var idx = list.FindIndex(r => r.Id == entity.Id);
        if (idx < 0) return false;
        list[idx] = entity;
        Save(list);
        return true;
    }

    public bool Remove(int id)
    {
        var list = Load();
        var removed = list.RemoveAll(r => r.Id == id) > 0;
        if (removed) Save(list);
        return removed;
    }

    // ---------- IReadRepository Implementação ----------
    public CurrencyRate? GetById(int id)
    {
        return Load().FirstOrDefault(r => r.Id == id);
    }

    public IReadOnlyList<CurrencyRate> ListAll()
    {
        return Load();
    }

    // ---------- Helpers (Fase 7) ----------
    private List<CurrencyRate> Load()
    {
        if (!File.Exists(_path)) return new List<CurrencyRate>();
        var json = File.ReadAllText(_path, Encoding.UTF8);
        if (string.IsNullOrWhiteSpace(json)) return new List<CurrencyRate>();
        try
        {
            return JsonSerializer.Deserialize<List<CurrencyRate>>(json, _opts) ?? new List<CurrencyRate>();
        }
        catch
        {
            return new List<CurrencyRate>();
        }
    }

    private void Save(List<CurrencyRate> list)
    {
        var ordered = list.OrderBy(x => x.Id).ToList();
        var json = JsonSerializer.Serialize(ordered, _opts);
        File.WriteAllText(_path, json, Encoding.UTF8);
    }
}
