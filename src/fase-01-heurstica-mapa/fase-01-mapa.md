# Fase 1 — Heurística e mapa (Tema: Conversão de Moedas)

## Problema escolhido
Conversão de moedas com múltiplas políticas (fixa, personalizada, comercial/turismo), conversão BRL↔moedas e entre moedas estrangeiras.

---

## Visão procedural (fluxo)
- Entrada: valor, moeda origem, moeda destino, política.
- Normalizar valor.
- Obter cotação:
  - Se política = personalizado -> usar cotação fornecida.
  - Se política = comercial/turismo -> usar cotação base ± spread.
  - Se cotação direta disponível -> preferir.
- Executar conversão (se origem != BRL e destino != BRL: converter via BRL ou direta).
- Aplicar arredondamento.
- Registrar log.

Problemas: muitos `if/switch` para políticas, tipos de conversão e fontes de cotação.

---

## OO sem interface (planejado para Fase 3)
- `MoneyConverterBase` (abstract) — define `Convert(value, from, to, policy)`.
- Concretos:
  - `BrlToForeignConverter`
  - `ForeignToBrlConverter`
  - `DirectForeignConverter`
- `RateProvider` — responsável por fornecer cotações (fixas, personalizadas, direta).
- `Formatter` — aplica arredondamento e formatação de saída.
- Cliente compõe concretos (a composição será ponto de rigidez e discutida na análise).

---

## Futuro (com interface / injeção)
- Definir `IMoneyConverter` ou `IRateProvider` e usar fábrica/DI para remover conhecimento de concretos do cliente.

