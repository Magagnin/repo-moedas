using System;
using System.Collections.Generic;
using Xunit;

// FASE 10 — Testes que provam a segurança das refatorações.
// Aqui usamos dublês simples para validar comportamento antes/depois.

public class CheirosAntidotosTests
{
    // ----------------------------------------------------------------------
    // 1) Downcast removido → uso de contrato específico (ITextFormatter)
    // ----------------------------------------------------------------------
    public interface ITextFormatter
    {
        string Apply(string s);
    }

    private sealed class FakeFormatter : ITextFormatter
    {
        public string Apply(string s) => $"[{s}]";
    }

    [Fact]
    public void DowncastAntidoto_DeveUsarFormatterSemIsOuAs()
    {
        ITextFormatter fmt = new FakeFormatter();
        var result = fmt.Apply("ABC");

        Assert.Equal("[ABC]", result);
    }

    // ----------------------------------------------------------------------
    // 2) Interface gorda → segregação (ISP)
    // ----------------------------------------------------------------------
    public interface IReadRepository<T, TId>
    {
        T? GetById(TId id);
        IReadOnlyList<T> ListAll();
    }

    public interface IWriteRepository<T, TId>
    {
        T Add(T entity);
        bool Update(T entity);
        bool Remove(TId id);
    }

    private sealed class ReadOnlyFakeRepo : IReadRepository<int, int>
    {
        public IReadOnlyList<int> ListAll() => new List<int> { 1, 2, 3 };
        public int? GetById(int id) => id == 1 ? 1 : null;
    }

    [Fact]
    public void IspAntidoto_ClienteDeLeituraNaoPrecisaDeMetodosDeEscrita()
    {
        IReadRepository<int, int> repo = new ReadOnlyFakeRepo();
        var all = repo.ListAll();

        Assert.Equal(new[] { 1, 2, 3 }, all);
    }

    // ----------------------------------------------------------------------
    // 3) Decisão espalhada → Catálogo único
    // ----------------------------------------------------------------------
    private interface IModeBehavior { string Run(); }

    private sealed class UpperMode : IModeBehavior { public string Run() => "UP"; }
    private sealed class LowerMode : IModeBehavior { public string Run() => "LOW"; }
    private sealed class DefaultMode : IModeBehavior { public string Run() => "DEFAULT"; }

    private static class ModeCatalog
    {
        public static IModeBehavior Resolve(string m) =>
            m.ToLowerInvariant() switch
            {
                "upper" => new UpperMode(),
                "lower" => new LowerMode(),
                _ => new DefaultMode()
            };
    }

    [Fact]
    public void CatalogoAntidoto_DeveResolverPolimorficamente()
    {
        Assert.Equal("UP", ModeCatalog.Resolve("upper").Run());
        Assert.Equal("LOW", ModeCatalog.Resolve("lower").Run());
        Assert.Equal("DEFAULT", ModeCatalog.Resolve("???").Run());
    }

    // ----------------------------------------------------------------------
    // 4) Lista gigante de parâmetros → Policy Object
    // ----------------------------------------------------------------------
    private sealed record ExportPolicy(bool Zip, int Level);

    private static class Exporter
    {
        public static string Export(string path, ExportPolicy policy)
        {
            return $"{path} | zip={policy.Zip} | lvl={policy.Level}";
        }
    }

    [Fact]
    public void PolicyObjectAntidoto_DeveAgruparParametrosEmObjetoUnico()
    {
        var policy = new ExportPolicy(true, 5);
        var result = Exporter.Export("file.txt", policy);

        Assert.Equal("file.txt | zip=True | lvl=5", result);
    }

    // ----------------------------------------------------------------------
    // 5) Testes lentos com I/O → Dublê de armazenamento (in-memory)
    // ----------------------------------------------------------------------
    private sealed class InMemoryStore
    {
        private readonly Dictionary<int, string> _map = new();

        public void Save(int id, string value) => _map[id] = value;
        public string? Load(int id) => _map.TryGetValue(id, out var v) ? v : null;
    }

    [Fact]
    public void IoAntidoto_DeveUsarArmazenamentoEmMemoriaEmVezDeArquivo()
    {
        var store = new InMemoryStore();
        store.Save(1, "ABC");

        Assert.Equal("ABC", store.Load(1));
    }
}
