# Fase 7 — Repository JSON (Conversão de Moedas)

## Domínio
`CurrencyRate` — Id, From, To, Rate

## Formato JSON (escolha A: lista)
- Arquivo: `currency_rates.json` (no diretório do app)
- Estrutura: array de objetos
- Exemplo:
```json
[
  {
    "id": 1,
    "from": "USD",
    "to": "BRL",
    "rate": 5.2
  }
]

