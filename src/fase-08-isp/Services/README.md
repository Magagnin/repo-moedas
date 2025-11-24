# Fase 8 — Interface Segregation Principle (ISP)

## Domínio
`CurrencyRate` — Id, From, To, Rate

---

## 📐 Princípio Aplicado: Interface Segregation Principle (ISP)

[cite_start]Esta fase refatora o projeto da Fase 7 aplicando o **ISP**, garantindo que nenhum cliente seja forçado a depender de métodos que não usa[cite: 3, 4].

### 1. Diagnóstico da Interface Gorda
[cite_start]A interface `IRepository<T, TId>` da Fase 7 foi identificada como "gorda"[cite: 3], pois combinava métodos de leitura (`GetById`, `ListAll`) e escrita (`Add`, `Update`, `Remove`).

### 2. Contratos Segregados
[cite_start]A interface gorda foi dividida em contratos coesos por capacidade[cite: 2]:

* **`IReadRepository<T, TId>`**: Contém apenas métodos de **consulta** (`GetById`, `ListAll`).
* **`IWriteRepository<T, TId>`**: Contém apenas métodos de **comando/mutação** (`Add`, `Update`, `Remove`).

### 3. Cliente Refatorado
[cite_start]O `CurrencyRateService` foi ajustado para depender apenas dos contratos mínimos necessários para suas operações[cite: 4].
* O serviço agora recebe **dois** contratos: `IReadRepository` e `IWriteRepository`.
* Métodos como `ListAll` dependem exclusivamente de `IReadRepository`.
* Métodos como `Register` (Add) dependem exclusivamente de `IWriteRepository`.

### 4. Dublês Mínimos (Testes)
[cite_start]A segregação facilita a criação de dublês de teste (Fakes/Mocks) mínimos, sem métodos inúteis[cite: 4, 5], por exemplo:
* Um `ReadOnlyFake` implementa apenas `IReadRepository` para testar operações de consulta.

---

## 🛠️ Detalhes da Implementação

* **Implementação**: `JsonCurrencyRateRepository` (mantido da Fase 7, mas agora implementando *ambas* as interfaces `IReadRepository` e `IWriteRepository`).
* **Arquivo JSON**: `currency_rates_isp.json` (no diretório do app).
* **Política de Id**: Id informado pelo usuário (opção A).

---

## 🚀 Como Executar

O projeto demonstra a injeção dos contratos segregados no serviço.

```bash
cd src/fase-08-isp
dotnet run
