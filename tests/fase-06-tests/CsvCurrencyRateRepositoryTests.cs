using System;
using System.IO;
using System.Linq;
using Xunit;
using Fase06.RepositoryCsv.Domain;
using Fase06.RepositoryCsv.Repository;

public class CsvCurrencyRateRepositoryTests
{
    private static string CreateTempPath()
    {
        var file = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".csv");
        File.WriteAllText(file, string.Empty); // garante arquivo vazio
        return file;
    }

    private static CsvCurrencyRateRepository CreateRepo(string path) => new CsvCurrencyRateRepository(path);

    [Fact]
    public void ListAll_WhenFileDoesNotExist_ShouldReturnEmpty()
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".csv");
        if (File.Exists(path)) File.Delete(path);
        var repo = CreateRepo(path);
        var all = repo.ListAll();
        Assert.Empty(all);
    }

    [Fact]
    public void Add_Then_ListAll_ShouldPersistInFile_WithEscapes()
    {
        var path = CreateTempPath();
        var repo = CreateRepo(path);

        // title contains comma, from contains quotes to test escaping
        repo.Add(new CurrencyRate(1, "US,D", "BRL", 5.2m));
        repo.Add(new CurrencyRate(2, "EUR", "BRL", 6.1m));
        var all = repo.ListAll();
        Assert.Equal(2, all.Count);
        Assert.Equal(1, all[0].Id);
        Assert.Equal("US,D", all[0].From);
    }

    [Fact]
    public void GetById_Existing_ShouldReturnEntity()
    {
        var path = CreateTempPath();
        var repo = CreateRepo(path);
        repo.Add(new CurrencyRate(1, "USD", "BRL", 5.2m));
        var found = repo.GetById(1);
        Assert.NotNull(found);
        Assert.Equal("USD", found!.From);
    }

    [Fact]
    public void GetById_Missing_ShouldReturnNull()
    {
        var path = CreateTempPath();
        var repo = CreateRepo(path);
        var found = repo.GetById(99);
        Assert.Null(found);
    }

    [Fact]
    public void Update_Existing_ShouldPersistChanges()
    {
        var path = CreateTempPath();
        var repo = CreateRepo(path);
        repo.Add(new CurrencyRate(1, "USD", "BRL", 5.2m));
        var updated = repo.Update(new CurrencyRate(1, "USD", "BRL", 5.5m));
        Assert.True(updated);
        Assert.Equal(5.5m, repo.GetById(1)!.Rate);
    }

    [Fact]
    public void Update_Missing_ShouldReturnFalse()
    {
        var path = CreateTempPath();
        var repo = CreateRepo(path);
        var updated = repo.Update(new CurrencyRate(1, "USD", "BRL", 5.5m));
        Assert.False(updated);
    }

    [Fact]
    public void Remove_Existing_ShouldDeleteFromFile()
    {
        var path = CreateTempPath();
        var repo = CreateRepo(path);
        repo.Add(new CurrencyRate(1, "USD", "BRL", 5.2m));
        var removed = repo.Remove(1);
        Assert.True(removed);
        Assert.Empty(repo.ListAll());
    }

    [Fact]
    public void Remove_Missing_ShouldReturnFalse()
    {
        var path = CreateTempPath();
        var repo = CreateRepo(path);
        var removed = repo.Remove(99);
        Assert.False(removed);
    }
}

