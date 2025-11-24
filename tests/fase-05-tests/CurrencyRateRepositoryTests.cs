using Xunit;
using Fase05.RepositoryInMemory.Domain;
using Fase05.RepositoryInMemory.Repository;

public class CurrencyRateRepositoryTests
{
    private static InMemoryRepository<CurrencyRate, int> CreateRepo()
        => new InMemoryRepository<CurrencyRate, int>(r => r.Id);

    [Fact]
    public void Add_Then_ListAll_ShouldReturnOneItem()
    {
        var repo = CreateRepo();
        repo.Add(new CurrencyRate(1, "USD", "BRL", 5.2m));
        var all = repo.ListAll();
        Assert.Single(all);
        Assert.Equal(1, all[0].Id);
    }

    [Fact]
    public void GetById_Existing_ShouldReturnEntity()
    {
        var repo = CreateRepo();
        repo.Add(new CurrencyRate(1, "USD", "BRL", 5.2m));
        var found = repo.GetById(1);
        Assert.NotNull(found);
        Assert.Equal("USD", found!.From);
    }

    [Fact]
    public void GetById_Missing_ShouldReturnNull()
    {
        var repo = CreateRepo();
        var found = repo.GetById(99);
        Assert.Null(found);
    }

    [Fact]
    public void Update_Existing_ShouldReturnTrue()
    {
        var repo = CreateRepo();
        repo.Add(new CurrencyRate(1, "USD", "BRL", 5.2m));
        var updated = repo.Update(new CurrencyRate(1, "USD", "BRL", 5.5m));
        Assert.True(updated);
        Assert.Equal(5.5m, repo.GetById(1)!.Rate);
    }

    [Fact]
    public void Update_Missing_ShouldReturnFalse()
    {
        var repo = CreateRepo();
        var updated = repo.Update(new CurrencyRate(1, "USD", "BRL", 5.5m));
        Assert.False(updated);
    }

    [Fact]
    public void Remove_Existing_ShouldReturnTrue()
    {
        var repo = CreateRepo();
        repo.Add(new CurrencyRate(1, "USD", "BRL", 5.2m));
        var removed = repo.Remove(1);
        Assert.True(removed);
        Assert.Empty(repo.ListAll());
    }

    [Fact]
    public void Remove_Missing_ShouldReturnFalse()
    {
        var repo = CreateRepo();
        var removed = repo.Remove(99);
        Assert.False(removed);
    }
}

