using Xunit;

public class ConversionServiceTests
{
    [Fact]
    public void ConvertAndFormat_UsesInjectedConverter()
    {
        // arrange
        var fake = new FakeMoneyConverter();
        fake.ReturnedValue = 42.5m; // valor que o fake retorna
        var service = new ConversionService(fake);

        // act
        string outp = service.ConvertAndFormat(10m, "BRL", "USD", "FIXA", 2);

        // assert: o fake recebeu os parâmetros corretos
        Assert.Equal(10m, fake.LastAmount);
        Assert.Equal("BRL", fake.LastFrom);
        Assert.Equal("USD", fake.LastTo);
        Assert.Equal("FIXA", fake.LastPolicy);

        // e o retorno formatado bate com o valor do fake (42.5 -> "42.5 USD" com 2 decimais)
        Assert.Equal("42.5 USD", outp);
    }
}

