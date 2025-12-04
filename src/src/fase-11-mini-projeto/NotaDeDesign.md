# Nota de Design — Fase 11

Decisões principais:
- Aplicamos ISP (IReadRepository / IWriteRepository) para reduzir acoplamento.
- JsonCurrencyRateRepository implementa ambos, permitindo uso unificado.
- InMemoryRepository usado para testes rápidos e dublês.
- CurrencyRateService depende somente de contratos (facilita testes e substituição de storage).
