# Trabalho por Fases — Conversão de Moedas (Fase 0 → Fase 3)

**Tema:** Conversão de moedas (BRL ⇄ USD, EUR, GBP, JPY, etc.)  
**Fases entregues:** 0, 1, 2, 3

## Sumário

## 📄 Sumário
- [Fase 0 — Orientações](src/fase-00-orientacoes/fase-00.md)  
- [Fase 1 — Heurística (mapa)](docs/arquitetura/fase-01-mapa.md)  
- [Fase 2 — Procedural mínimo (formatador de texto)](src/fase-02-procedural/README.md)  
- [Fase 3 — OO sem interface (formatador de texto)](src/fase-03-oo-sem-interface/README.md)  
- [Fase 4 — Interface plugável e testável (Conversão de Moedas)](src/fase-04-com-interfaces/README.md)
- [Fase 5 — Repository InMemory (Conversão de Moedas)](src/fase-05-repository-inmemory/README.md)


Sobre o projeto

Este repositório demonstra a evolução completa de um projeto em múltiplas fases:

Fase 2: Separação entre processo fixo e variável (procedural).

Fase 3: Primeira versão OO, sem uso de interfaces.

Fase 4: Arquitetura plugável com interfaces, catálogo e testes com dublês.

Fase 5: Introdução de Repository Pattern com implementação InMemory, domínio CurrencyRate, serviço de aplicação e testes unitários completos.

## Como executar (recomendado .NET 6.0+)
Na raiz do repositório:

```bash
# executar versão procedural
cd src/fase-02-procedural-conversao
dotnet run

# executar versão OO sem interface
cd ../fase-03-oo-sem-interface-conversao
dotnet run

#
cd src/fase-04-com-interfaces
dotnet run

#
cd src/fase-05-repository-inmemory
dotnet run


