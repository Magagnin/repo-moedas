# Fase 10 — Cheiros e Antídotos

Abaixo estão **7 cheiros reais** encontrados no projeto de Conversão de Moedas, cada um com:

✔ ANTES  
✔ DEPOIS  
✔ Antídoto  
✔ Princípio SOLID aplicado  
✔ Teste que demonstra segurança

---

---

# 🧪 **Cheiro 1 — Interface Gorda (IRepository)**
### ✔ Problema
O contrato tinha **todos os métodos**: Add, GetById, ListAll, Update, Remove.  
O cliente precisava apenas de alguns.

### ❌ ANTES
```csharp
public interface IRepository<T, TId>
{
    T Add(T e);
    T? GetById(TId id);
    IReadOnlyList<T> ListAll();
    bool Update(T e);
    bool Remove(TId id);
}

✅ DEPOIS

public interface IReadRepository<T, TId>
{
    T? GetById(TId id);
    IReadOnlyList<T> ListAll();
}

public interface IWriteRepository<T, TId>
{
    T Add(T e);
    bool Update(T e);
    bool Remove(TId id);
}

🎯 Antídoto

ISP — Interface Segregation Principle

🧪 Teste que garante segurança
Assert.True(new ReadOnlyFake().ListAll().Count == 0);

🧪 Cheiro 2 — Testes Lentíssimos com I/O Real
❌ ANTES
var repo = new CsvCurrencyRateRepository("taxas.csv"); // usava DISCO!

✅ DEPOIS
var repo = new InMemoryCurrencyRateRepository();
🎯 Antídoto

Mover I/O para bordas + usar dublês.

🧪 Teste
var repo = new InMemoryCurrencyRateRepository();
Assert.Empty(repo.ListAll());


🧪 Cheiro 3 — Downcast no Cliente
❌ ANTES
if (fmt is UpperCaseFormatter up)
    Console.WriteLine(up.Apply(s));
else if (fmt is LowerCaseFormatter lo)
    Console.WriteLine(lo.Apply(s));

✅ DEPOIS
void Render(ITextFormatter fmt, string s)
    => Console.WriteLine(fmt.Apply(s));

🎯 Antídoto

DIP + Polimorfismo

🧪 Teste
Assert.Equal("ABC", fake.Apply("abc"));

🧪 Cheiro 4 — Decisão espalhada pelo código
❌ ANTES
if (mode == "UPPER") ...
else if (mode == "lower") ...

✅ DEPOIS
public static class FormatterCatalog
{
   public static ITextFormatter Resolve(string m)
       => m switch {
          "UPPER" => new Upper(),
          "lower" => new Lower(),
           _ => new Passthrough()
       };
}

🎯 Antídoto

Composição centralizada.


🧪 Cheiro 5 — Contrato Frágil no CurrencyRateService
❌ ANTES
public CurrencyRate Register(CurrencyRate r)
{
    if(r.Id <= 0 || r.Rate <= 0 || r.From == null || ...)
        throw new Exception();
    return _repo.Add(r);
}

Problema:

Método fazia validação + negócio + persistência.

✅ DEPOIS
private static void Validate(CurrencyRate r) { ... }

public CurrencyRate Register(CurrencyRate r)
{
    Validate(r);
    return _write.Add(r);
}

🎯 Antídoto

SRP — Single Responsibility Principle


🧪 Cheiro 6 — Long Parameter List
❌ ANTES
UpdateRate(id, from, to, rate, forceValidation, logChanges, mode);

👍 Problema

Método difícil de entender e manter.

✅ DEPOIS
public sealed record UpdatePolicy(bool ForceValidation, bool LogChanges, string Mode);

UpdateRate(id, from, to, rate, new UpdatePolicy(true, false, "safe"));

🎯 Antídoto

Policy Object


🧪 Cheiro 7 — Explosão de Subclasses
❌ ANTES

Várias classes iguais mudando apenas valores.

✅ DEPOIS
public sealed class ConversionStrategy
{
    public decimal Apply(decimal rate, decimal amount)
        => rate * amount;
}

🎯 Antídoto

Strategy Pattern
