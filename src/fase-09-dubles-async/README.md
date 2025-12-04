# Fase 9 — Dublês Avançados e Testes Assíncronos (async/stream/tempo)

Nesta fase aplicamos:
- Testes assíncronos
- Dublês sofisticados (FakeReader, FakeWriter, FakeClock)
- Controle determinístico de tempo
- Retentativa com backoff sem usar Thread.Sleep
- Testes de sucesso, erro, cancelamento e erro no meio do stream

Estrutura:
- IClock, IIdGenerator, IAsyncReader, IAsyncWriter
- PumpService<T> (core da fase)
- Testes cobrindo 5 cenários:
  - Sucesso
  - Retentativa
  - Cancelamento
  - Stream vazio
  - Erro no meio do stream

Como executar:
cd src/fase-09-dubles-async
dotnet run

cd tests/fase-09-tests
dotnet test
