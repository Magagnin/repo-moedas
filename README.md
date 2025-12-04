Sobre o Projeto — Conversão de Moedas (Fases 0 a 7)

Este repositório reúne a evolução completa de um sistema de Conversão de Moedas, construído seguindo as fases definidas nas Lousas de Arquitetura Orientada por Interfaces (Fases 0 a 7).
Cada fase representa uma etapa real da evolução de um software, começando do procedural até uma arquitetura profissional com repositórios persistentes, testes e múltiplos formatos de armazenamento.

O domínio escolhido é: CurrencyRate, com os campos:

Id – identificador único

From – moeda de origem (ex.: USD)

To – moeda de destino (ex.: BRL)

Rate – taxa numérica (ex.: 5.20)

📘 Fase 0 — Orientações

Primeiros direcionamentos sobre processo fixo, processo variável e como o sistema evoluiria ao longo das fases seguintes. Sem código.

🧭 Fase 1 — Mapa Heurístico

Primeira visualização do sistema:

fluxo de conversão

interações básicas

primeiros pensamentos sobre as operações

Ainda sem implementação.

💻 Fase 2 — Versão Procedural

Primeira implementação funcional:

funções simples

sem classes, sem objetos

lógica totalmente direta e rígida

É o núcleo mínimo do sistema.

🧱 Fase 3 — Programação Orientada a Objetos (sem interfaces)

O código é organizado em classes:

Conversor

MenuApp

serviços básicos internos

Ainda não há interfaces, logo o sistema é rígido e difícil de substituir comportamentos.

🧩 Fase 4 — Interfaces Plugáveis e Testáveis

Grande salto de arquitetura:

criação de interfaces

implementação de componentes plugáveis

inversão de dependência (DIP)

testes unitários reais usando fakes

composition root para montar tudo

O sistema agora é flexível e testável.

🗂️ Fase 5 — Repository InMemory

Primeira aparição de persistência (simulada):

criação da entidade CurrencyRate

criação do contrato genérico:
IRepository<T, TId>

implementação InMemory usando Dictionary<int, T>

CurrencyRateService com regras de negócio e validação

testes unitários completos cobrindo CRUD

Nenhum arquivo ainda—tudo vive na memória do processo.

📁 Fase 6 — Repository CSV (Persistência em arquivo)

Agora o sistema persiste dados no disco de verdade:

implementação CsvCurrencyRateRepository

arquivo: currency_rates.csv

padrão do CSV com cabeçalho fixo

serialização e desserialização manuais

serviço inalterado (graças ao contrato da interface)

testes cobrindo:

criação do arquivo

persistência real

leitura e escrita consistentes

erro em linhas inválidas tratado corretamente

Esta fase introduz uma forma real de armazenamento.

🗃️ Fase 7 — Repository JSON (System.Text.Json)

Evolução da persistência: agora o repositório armazena os dados em JSON, usando a API oficial do .NET.

Nesta fase foram implementados:

✔️ JsonCurrencyRateRepository

Armazenamento em arquivo JSON

Estrutura escolhida: lista (array)

Formatação:

[
  {
    "id": 1,
    "from": "USD",
    "to": "BRL",
    "rate": 5.2
  }
]


Usa System.Text.Json com:

CamelCase

WriteIndented

ignorar nulls

Lida com:

arquivo inexistente

arquivo vazio

arquivo corrompido

Id definido pelo usuário (Fase 5 e 6 mantida)

✔️ Serviço de Domínio

Reutilizado sem alterações (CurrencyRateService), mostrando o ganho do DIP.

✔️ Programa console (Program.cs)

Menu completo para CRUD usando JSON.

✔️ Testes de Integração

100% focados no repositório JSON:

arquivo inexistente → lista vazia

Add deve persistir corretamente

GetById existente/ausente

Update existente/ausente

Remove persistente

arquivo corrompido deve retornar lista vazia

Cada teste usa arquivos temporários para evitar efeitos colaterais.

✔️ Objetivo da fase

Demonstrar que, usando interfaces, mudar o formato de persistência não exige mudar o serviço, nem o domínio.

O contrato garante a estabilidade da arquitetura.

⚖️ Fase 8 — Interface Segregation Principle (ISP)
Esta fase é focada na refatoração arquitetural para aplicar o Princípio da Segregação de Interfaces (ISP), o quarto princípio do SOLID. O objetivo é eliminar o contrato "gordo" e garantir que os clientes dependam apenas dos métodos que realmente utilizam.

Nesta fase foram implementados:

✔️ Segregação de Contratos: O contrato genérico IRepository<T, TId> (leitura e escrita) foi quebrado em dois contratos mínimos e coesos:

IReadRepository<T, TId> (apenas GetById, ListAll).

IWriteRepository<T, TId> (apenas Add, Update, Remove).

✔️ Cliente Refatorado: O CurrencyRateService foi ajustado para depender de ambos os contratos segregados em seu construtor, utilizando apenas o necessário para cada operação (ex.: ListAll usa apenas IReadRepository).

✔️ Implementação Unificada: O JsonCurrencyRateRepository (da Fase 7) foi adaptado para implementar ambas as novas interfaces (IReadRepository e IWriteRepository), mantendo a lógica de persistência JSON.

✔️ Dublês Mínimos em Testes: Criação de ReadOnlyFake e WriteOnlyFake nos testes de serviço para demonstrar que é possível criar dublês que implementam apenas o subconjunto de métodos exigidos pelo cliente, simplificando os testes.

✔️ Nota de Design: Documentação obrigatória (NotaDeDesign.md) explicando o diagnóstico da interface gorda, a segregação escolhida e seus efeitos na arquitetura.

Objetivo da Fase: Demonstrar que a aplicação do ISP reduz o acoplamento entre o cliente e o contrato, facilita a composição e simplifica drasticamente a criação de componentes para testes.

⚙️ Fase 9 — Dublês Avançados e Testes Assíncronos (async/stream/tempo)

A Fase 9 introduz testes assíncronos reais, streams assíncronos (IAsyncEnumerable), dublês avançados, além de controle de tempo e retentativa sem usar Thread.Sleep.
O objetivo é consolidar o design orientado a costuras, permitindo testar cenários complexos sem depender de I/O real, relógio real ou tempo real.

✔️ O que foi implementado na Fase 9
1. Contratos mínimos para costuras essenciais

Foram introduzidos contratos bem pequenos e altamente substituíveis:

IClock → relógio controlável por teste

IIdGenerator → geração previsível de IDs

IAsyncReader<T> → leitura de stream assíncrono

IAsyncWriter<T> → escrita assíncrona controlada

Esses contratos permitem simular qualquer dependência externa real (como arquivos, streams, sockets, tempo, etc.) sem acoplamento.

2. Serviço Assíncrono (PumpService)

Foi criado um serviço genérico com suporte a:

leitura contínua via IAsyncEnumerable<T>

retentativa automática com backoff simulado (sem esperar de verdade)

cancelamento via CancellationToken

supervisão de erros no meio do stream

Esse serviço representa um cenário real de sistemas modernos — pipelines, ETL, filas, mensagens etc.

3. Dublês avançados criados para testes
✔ FakeClock

Relógio controlado pelo teste, avançando manualmente.

✔ FakeReader

Produz um stream assíncrono:

normal

vazio

com erro no meio

até mesmo infinito (controlado)

✔ FakeWriter

Pode ser configurado para:

sempre escrever

falhar X vezes

falhar sempre

respeitar cancelamento

Tudo isso sem acessar disco nem rede.

4. Testes Unitários completos

Todos os cenários definidores da fase foram implementados:

Cenário	Resultado esperado
✔Sucesso simples	O PumpService processa todos os itens
✔Retentativa com erro temporário	Após N falhas, sucesso; sem Sleep real
✔Cancelamento	Interrompe imediatamente e retorna parcial
✔Stream vazio	Retorno = 0 sem erros
✔Erro no meio do stream	Exceção propagada corretamente
✔Backoff baseado em clock fake	Teste verifica avanço de tempo

Todos os testes são 100% determinísticos, independentemente da velocidade da máquina.

5. README da fase criado

Explicação técnica da fase, contratos, motivação e arquitetura interna.

6. Pasta criada
src/fase-09-dubles-async/
tests/fase-09-tests/


Inclui:

contratos

PumpService

fakes

testes xUnit

csproj

README

🎯 Objetivo da Fase 9

Garantir que o software pode ser testado em cenários complexos e realistas sem I/O real, com total controle sobre:

tempo

streams

políticas

cancelamento

Esta fase fecha o ciclo de maturidade arquitetural e de testes, tornando o projeto apto a padrões profissionais.

🌟 Benefícios entregues

Sistema extremamente testável

Testes rápidos, determinísticos e confiáveis

Arquitetura orientada a costuras

Independência total de I/O (arquivos, rede, relógio)

Suporte a pipelines e tecnologias modernas (async/await, streaming)


