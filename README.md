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
