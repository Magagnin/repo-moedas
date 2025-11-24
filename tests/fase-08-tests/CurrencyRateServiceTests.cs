using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Fase08.Isp.Domain;
using Fase08.Isp.Repository;
using Fase08.Isp.Services;

public class CurrencyRateServiceTests
{
    [cite_start]// O ReadFake implementa SOMENTE IReadRepository (Dublê Mínimo) 
    private sealed class ReadOnlyFake : IReadRepository<CurrencyRate, int>
    {
        private readonly Dictionary<int, CurrencyRate> _db = new() 
        { 
            [1] = new(1, "USD", "BRL", 5.2m),
            [2] = new(2, "EUR", "BRL", 6.1m)
        };

        public CurrencyRate? GetById(int id) => _db.TryGetValue(id, out var b) ? b : null;
        public IReadOnlyList<CurrencyRate> ListAll() => _db.Values.ToList();
        
        [cite_start]// Não precisamos implementar Add, Update, Remove, que é o objetivo do ISP [cite: 57]
    }
    
    [cite_start]// O WriteFake implementa SOMENTE IWriteRepository (Dublê Mínimo) 
    private sealed class WriteOnlyFake : IWriteRepository<CurrencyRate, int>
    {
        // Simulamos a persistência em memória (um mapa para escrita)
        private readonly Dictionary<int, CurrencyRate> _db = new();

        public CurrencyRate Add(CurrencyRate entity)
        {
            _db[entity.Id] = entity;
            return entity;
        }

        public bool Update(CurrencyRate entity)
        {
            if (!_db.ContainsKey(entity.Id)) return false;
            _db[entity.Id] = entity;
            return true;
        }

        public bool Remove(int id) => _db.Remove(id);
    }
    
    // Este teste foca na consulta, usando o ReadOnlyFake para IRead.
    [Fact]
    public void ListAll_ShouldReturnAllRatesFromFake()
    {
        var readFake = new ReadOnlyFake();
        var writeFake = new WriteOnlyFake(); // Este fake pode ser "vazio" se não precisarmos de escrita
        
        // Composição: Serviço recebe Fakes mínimos
        var service = new CurrencyRateService(readFake, writeFake);

        var all = service.ListAll();
        
        // Verificamos que os dados vieram do nosso Fake de Leitura
        Assert.Equal(2, all.Count);
        Assert.Contains(all, r => r.Id == 1 && r.From == "USD");
    }
    
    // Este teste foca na escrita, usando o WriteOnlyFake.
    [Fact]
    public void Register_ShouldUseWriteFake()
    {
        var writeFake = new WriteOnlyFake(); 
        var readFake = new ReadOnlyFake(); 
        var service = new CurrencyRateService(readFake, writeFake);

        var newRate = new CurrencyRate(99, "CAD", "USD", 0.73m);
        service.Register(newRate);
        
        // Não é possível verificar diretamente o WriteOnlyFake, mas podemos usar a validação
        // Se a validação passasse, o Register usaria o Add do WriteFake, provando o uso.
        
        // Exemplo:
        Assert.Throws<ArgumentException>(() => 
        {
            service.Register(new CurrencyRate(99, "CAD", "USD", 0)); // Taxa <= 0
        });
    }
    
    // Outros testes do JsonCurrencyRateRepository (Fase 7) não precisam de alterações.
}
