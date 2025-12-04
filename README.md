Sobre o Projeto — Conversão de Moedas (Fases 0 a 11)

Este repositório reúne a evolução completa de um sistema de Conversão de Moedas, construído seguindo as fases definidas nas Lousas de Arquitetura Orientada por Interfaces (Fases 0 a 11).

Cada fase representa uma etapa real da evolução de um software, começando do procedural até uma arquitetura profissional com:

✔ repositórios persistentes (CSV, JSON)
✔ princípios SOLID
✔ dublês avançados
✔ testes assíncronos
✔ refatorações com cheiros e antídotos
✔ mini-projeto final consolidado

O domínio escolhido é CurrencyRate, com os campos:

Id – identificador único

From – moeda de origem (ex.: USD)

To – moeda de destino (ex.: BRL)

Rate – taxa numérica (ex.: 5.20)

📘 Fase 0 — Orientações

Primeiros direcionamentos sobre:

processo fixo × variável

como o sistema evoluiria ao longo das fases

sem código

🧭 Fase 1 — Mapa Heurístico

Primeira visualização do sistema:

fluxo básico da conversão

entradas, saídas

primeiros passos do domínio

Ainda sem código.

💻 Fase 2 — Versão Procedural

Primeira implementação funcional:

funções simples, sem classes

lógica direta e rígida

núcleo mínimo do programa

🧱 Fase 3 — OO sem Interfaces

Código organizado em classes:

Conversor

MenuApp

CurrencyRate

Ainda sem interfaces → difícil de substituir comportamentos.

🧩 Fase 4 — Interfaces Plugáveis e Testáveis

Grande salto de arquitetura:

criação de interfaces

componentes plugáveis

inversão de dependência (DIP)

testes com fakes

composição no Catalog/Program

Agora o sistema é flexível.

🗂️ Fase 5 — Repository InMemory

Primeiro repositório real (simulado):

entidade CurrencyRate

contrato genérico IRepository<T, TId>

implementação InMemory

CurrencyRateService com validações

testes completos de CRUD

Nenhum arquivo físico ainda.

📁 Fase 6 — Repository CSV

Agora com persistência real:

CsvCurrencyRateRepository

arquivo currency_rates.csv

serialização e desserialização manual

serviço permanece o mesmo

testes cobrindo leitura, escrita, erros

🗃️ Fase 7 — Repository JSON (System.Text.Json)

Evolução da persistência:

JsonCurrencyRateRepository

arquivo JSON com camelCase

WriteIndented

tratamento de arquivo inexistente/vazio/corrompido

testes completos de integração

⚖️ Fase 8 — ISP (Interface Segregation Principle)

Refatoração para aplicar o ISP:

contrato “gordo” foi quebrado:

IReadRepository

IWriteRepository

CurrencyRateService passou a depender apenas do necessário

JsonRepository implementa ambos contratos

fakes mínimos criados

nota de design obrigatória

Melhora grande na coesão.

⚙️ Fase 9 — Dublês Avançados e Testes Assíncronos

Fase moderna focada em testabilidade avançada:

✔ Novos contratos essenciais:

IClock – controle de tempo

IIdGenerator – geração previsível

IAsyncReader<T> – leitura via stream assíncrono

IAsyncWriter<T> – escrita assíncrona

✔ Serviço Assíncrono (PumpService)

leitura contínua

retentativas sem usar Sleep

backoff controlado por clock fake

cancelamento com CancellationToken

tratamento de erro no meio do stream

✔ Dublês avançados

FakeClock

FakeReader com:

erro no meio

sequências infinitas

sequências vazias

FakeWriter configurável

✔ Testes completos

sucesso simples

retentativas

stream vazio

cancelamento

erro no meio

backoff baseado em clock fake

✔ Pasta da fase
src/fase-09-dubles-async/
tests/fase-09-tests/

🧼 Fase 10 — Cheiros e Antídotos (Refatorações profissionais)

A Fase 10 foca em detectar cheiros de código e aplicar refatorações pequenas e seguras, como um desenvolvedor profissional faria diariamente.

Foi seguida integralmente a Lousa da Fase 10.

✔ Cheiros identificados e corrigidos:
1. Parâmetros demais / SRP quebrado

Métodos com responsabilidades duplicadas foram quebrados.

A validação do CurrencyRate foi isolada em CurrencyRateValidator.

2. Condições complexas

Ifs longos foram substituídos por early-return.

Métodos foram reduzidos a blocos menores e mais legíveis.

3. Funções grandes

Program.cs separado em camadas pequenas.

Repositórios ganharam métodos auxiliares privados.

4. Nomes ruins

renomeação para nomes claros e autoexplicativos

From → SourceCurrency

To → TargetCurrency
(nomes permanecem compatíveis com JSON e CSV)

5. Código morto removido

variáveis não utilizadas

métodos redundantes

imports desnecessários

6. Exceções genéricas

trocadas por ArgumentException, InvalidDataException, InvalidOperationException.

7. Acesso externo duplicado

padrões de repetição no JsonRepository foram encapsulados.

✔ Pastas criadas
src/fase-10-refatoracoes/
tests/fase-10-tests/

✔ Testes garantem que:

nada mudou no comportamento

apenas o design foi melhorado

cobertura continua intacta

🌟 Fase 11 — Mini-projeto de Consolidação (Completo)

Fase final, integrando tudo em um projeto profissional completo.

✔ Conteúdos da Fase 11
1. Domínio completo:

CurrencyRate (record)

regras validadas

serviço consolidado

2. Repositórios reutilizados

InMemory

JSON com design final

ambos implementam os contratos segregados

3. Aplicação Console funcional

registra moedas

lista taxas

renomeia

remove

grava em JSON

4. Testes completos

testes de serviço

integração do repositório JSON

5. Nota de design

mostrando como as fases anteriores garantiram:

testabilidade

flexibilidade

separação de responsabilidades

evolução sem quebrar nada

6. Estrutura da Fase 11
src/fase-11-mini-projeto/
tests/fase-11-tests/

Inclui:

Program.cs

CurrencyRateService

Repositórios

Testes de unidade e integração

Projeto .csproj

✔ Estado Final (Fases 0 a 11 concluídas)

O projeto agora é:

🏆 totalmente modular
🏆 profissional
🏆 facilmente testável
🏆 pronto para extensões (API, Web, Banco de Dados)
🏆 exemplo real de Arquitetura por Fases
