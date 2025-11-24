using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Fase06.RepositoryCsv.Domain;

namespace Fase06.RepositoryCsv.Repository;

/// <summary>
/// Repository CSV para CurrencyRate.
/// Formato CSV (UTF-8) com cabeçalho: Id,From,To,Rate
/// Polí­tica de Id: Id vem de fora (usuário/domínio).
/// Escape: aspas -> "" , campos com vírgula/aspas/quebras são colocados entre aspas.
/// </summary>
public sealed class CsvCurrencyRateRepository : IRepository<CurrencyRate, int>
{
    private readonly string _path;

    public CsvCurrencyRateRepository(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Path inválido", nameof(path));
        _path = path;
    }

    public CurrencyRate Add(CurrencyRate entity)
    {
        var list = Load();
        // se já existir id, substitui (comportamento compatível com lousa)
        list.RemoveAll(r => r.Id == entity.Id);
        list.Add(entity);
        Save(list);
        return entity;
    }

    public CurrencyRate? GetById(int id)
    {
        return Load().FirstOrDefault(r => r.Id == id);
    }

    public IReadOnlyList<CurrencyRate> ListAll()
    {
        return Load();
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

    // ---------- Helpers privados ----------

    private List<CurrencyRate> Load()
    {
        if (!File.Exists(_path)) return new List<CurrencyRate>();

        var lines = File.ReadAllLines(_path, Encoding.UTF8);
        if (lines.Length == 0) return new List<CurrencyRate>();

        var result = new List<CurrencyRate>();
        int start = 0;
        if (lines[0].StartsWith("Id,", StringComparison.OrdinalIgnoreCase)) start = 1;

        for (int i = start; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;
            var cols = SplitCsvLine(line);
            if (cols.Count < 4) continue;
            if (!int.TryParse(cols[0], out var id)) continue;
            var from = cols[1];
            var to = cols[2];
            if (!decimal.TryParse(cols[3], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var rate)) continue;
            result.Add(new CurrencyRate(id, from, to, rate));
        }

        return result;
    }

    private void Save(List<CurrencyRate> list)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id,From,To,Rate");
        foreach (var r in list.OrderBy(x => x.Id))
        {
            var id = r.Id.ToString();
            var from = Escape(r.From);
            var to = Escape(r.To);
            var rate = r.Rate.ToString("G", System.Globalization.CultureInfo.InvariantCulture);
            sb.Append(id).Append(',').Append(from).Append(',').Append(to).Append(',').Append(rate).AppendLine();
        }

        File.WriteAllText(_path, sb.ToString(), Encoding.UTF8);
    }

    private static string Escape(string value)
    {
        if (value == null) return string.Empty;
        var needsQuotes = value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r');
        var escaped = value.Replace("\"", "\"\"");
        return needsQuotes ? $"\"{escaped}\"" : escaped;
    }

    private static List<string> SplitCsvLine(string line)
    {
        var result = new List<string>();
        if (string.IsNullOrEmpty(line))
        {
            result.Add(string.Empty);
            return result;
        }

        var current = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            var c = line[i];
            if (inQuotes)
            {
                if (c == '"')
                {
                    // aspas escapada?
                    if (i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = false;
                    }
                }
                else
                {
                    current.Append(c);
                }
            }
            else
            {
                if (c == ',')
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else if (c == '"')
                {
                    inQuotes = true;
                }
                else
                {
                    current.Append(c);
                }
            }
        }

        result.Add(current.ToString());
        return result;
    }
}

