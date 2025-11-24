# Nota de Design — Fase 8: Interface Segregation Principle (ISP)

## 1. Diagnóstico da Interface Gorda
* **Interface Gorda**: `IRepository<T, TId>` (usada na Fase 7).
* **Métodos**: `Add`, `GetById`, `ListAll`, `Update`, `Remove`.
* **Cliente em Foco**: `CurrencyRateService`.
* **Problema (Acoplamento)**: O `CurrencyRateService` precisava de *todos* os métodos do `IRepository` injetado (via Construtor), mesmo que usasse apenas um subconjunto em certas operações. [cite_start]Isso forçava qualquer dublê de teste a implementar métodos desnecessários[cite: 3, 55]. [cite_start]Por exemplo, se tivéssemos um `CatalogQuery` (somente consulta, como no exemplo da lousa [cite: 35]), ele seria forçado a depender de métodos de escrita.

## 2. Contratos Segregados Escolhidos
[cite_start]A interface foi segregada por **capacidade** (leitura/escrita), em vez de por camadas[cite: 64]:
1.  **`IReadRepository<CurrencyRate, int>`**: Contém apenas `GetById` e `ListAll` (Leitura).
2.  **`IWriteRepository<CurrencyRate, int>`**: Contém apenas `Add`, `Update`, e `Remove` (Escrita).

## 3. Efeitos da Segregação
* **`CurrencyRateService` Refatorado**: Passou a receber **dois** contratos injetáveis (`IReadRepository` e `IWriteRepository`) em seu construtor, dependendo apenas do que é estritamente necessário para cada operação (e.g., `ListAll` usa apenas `IReadRepository._readRepo.ListAll()`). [cite_start]**Redução de Acoplamento**[cite: 68].
* [cite_start]**Dublês Mínimos (Testes)**: Na fase de testes, agora é possível criar *Fakes* ou *Mocks* que implementam **apenas** `IReadRepository` (para testar consultas) ou `IWriteRepository` (para testar comandos), simplificando o teste do `CurrencyRateService` e acelerando os testes[cite: 4, 68].
* **Implementação (JsonCurrencyRateRepository)**: O repositório concreto (`JsonCurrencyRateRepository`) implementa **ambas** as novas interfaces, mantendo a responsabilidade de persistência.
