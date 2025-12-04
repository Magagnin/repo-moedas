using Xunit;
using Fase11.MiniProject.Domain;
using Fase11.MiniProject.Services;
using Fase11.MiniProject.Repository;

public class CurrencyRateServiceTests
{
    [Fact]
    public void Register_And_Find_Work_With_Fakes()
    {
        var write = new InMemoryRepository<CurrencyRate,int>(r=>r.Id);
        IReadRepository<CurrencyRate,int> read = write;
        var service = new CurrencyRateService(read, write);
        service.Register(new CurrencyRate(1, "USD", "BRL", 5.2m));
        var found = service.Find(1);
        Assert.NotNull(found);
        Assert.Equal("USD", found!.From);
    }

    [Fact]
    public void Rename_ReturnsFalse_WhenMissing()
    {
        var write = new InMemoryRepository<CurrencyRate,int>(r=>r.Id);
        IReadRepository<CurrencyRate,int> read = write;
        var service = new CurrencyRateService(read, write);
        var ok = service.Rename(99, "A", "B");
        Assert.False(ok);
    }
}
