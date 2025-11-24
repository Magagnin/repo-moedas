# Sobre o Projeto — Conversão de Moedas (Fases 0 a 6)

Este repositório reúne a evolução completa de um sistema de *Conversão de Moedas*, construído seguindo as fases definidas nas Lousas de Arquitetura Orientada por Interfaces (Fase 0 a Fase 6).  
Cada fase representa uma etapa real da evolução de um software, começando do nível mais simples (procedural) até uma arquitetura profissional com repositórios persistentes e testes automatizados.

O domínio escolhido é simples e direto: **CurrencyRate**, que representa uma taxa de conversão entre duas moedas.  
A estrutura da entidade é:

- **Id** – identificador único (informado pelo próprio usuário)
- **From** – moeda de origem (ex.: USD)
- **To** – moeda de destino (ex.: BRL)
- **Rate** – taxa numérica da conversão (ex.: 5.20)

---

## 📘 Fase 0 — Orientações
Primeiros direcionamentos sobre o que é processo fixo, processo variável e como as fases seguintes iriam evoluir o sistema.  
Sem código ainda.

---

## 🧭 Fase 1 — Mapa Heurístico
Desenho preliminar mostrando:
- Como o usuário interage com o programa
- O fluxo básico da conversão
- A ideia inicial do “motor” de conversão

Sem implementação ainda, apenas estudo e concepção.

---

## 💻 Fase 2 — Versão Procedural
Primeira implementação real do projeto, usando apenas funções.  
Aqui surgem:
- Funções para ler dados, processar texto e exibir saída
- Uma versão mínima funcional do sistema
- Nenhuma classe, nenhum objeto, nenhuma interface

É o nível mais baixo de abstração.

---

## 🧱 Fase 3 — Programação Orientada a Objetos (sem interfaces)
O código procedural evolui para uma estrutura orientada a objetos:
- Classes como `Conversor`, `MenuApp` e outras
- Método de conversão encapsulado
- Programa mais organizado, porém ainda rígido
- Nada pode ser substituído facilmente, pois **não há interfaces**

---

## 🧩 Fase 4 — Interfaces Plugáveis e Testáveis
O projeto passa a ser **desacoplado**:
- Interfaces como `IConversor` e `IFormatador`
- Implementações trocáveis sem alterar o resto do sistema
- Testes unitários com *dublês* (Fake) para simular comportamentos
- Uso de Catálogo (Composition Root) para montar o sistema

Aqui nasce a arquitetura profissional do projeto.

---

## 🗂️ Fase 5 — Repository InMemory (CurrencyRate)
Criação da entidade oficial **CurrencyRate** e introdução do padrão Repository.

Inclui:
- Contrato genérico `IRepository<T, TId>`
- Implementação in-memory usando `Dictionary<int,T>`
- Serviço de domínio `CurrencyRateService` com validações
- Testes unitários completos cobrindo:
  - Add  
  - List  
  - GetById  
  - Update  
  - Remove  

Nenhuma persistência em disco na Fase 5 — tudo fica apenas na memória.

---

## 📁 Fase 6 — Repository CSV (Persistência real em arquivo)
Evolução natural da Fase 5: agora os dados são realmente armazenados no disco.

- Implementação `CsvCurrencyRateRepository`
- Arquivo gerado: **currency_rates.csv**
- Cabeçalho padrão:  
