using System;
using System.IO;
using Xunit;
using Fase11.MiniProject.Repository;
using Fase11.MiniProject.Domain;

public class JsonRepositoryIntegrationTests
{
    private static string TempPath()
    {
        var p = Path.Combine(Path.GetTempPath(), Guid.NewGuid()+".json");
        return p;
    }

    [Fact]
    public void Add_Persists_And_Loads()
    {
        var p = TempPath();
        var repo = new JsonCurrencyRateRepository(p);
        repo.Add(new CurrencyRate(1, "USD", "BRL", 5.2m));
        var all = repo.ListAll();
        Assert.Single(all);
        File.Delete(p);
    }

    [Fact]
    public void Update_Works()
    {
        var p = TempPath();
        var repo = new JsonCurrencyRateRepository(p);
        repo.Add(new CurrencyRate(1, "USD", "BRL", 5.2m));
        var ok = repo.Update(new CurrencyRate(1, "USD", "BRL", 5.5m));
        Assert.True(ok);
        File.Delete(p);
    }
}
