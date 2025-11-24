# Análise: melhorias vs rigidez (Fase 3)

## Melhorias observadas
- Fluxo comum (validação, formatação) centralizado na base (`MoneyConverterBase`).
- Comportamentos variantes isolados em classes concretas (coesa responsabilidade).
- Testabilidade melhor: cada conversor pode ser testado isoladamente.

## Pontos ainda rígidos
- Cliente conhece concretos (ponto de composição local).
- Adicionar novo tipo de conversão exige alterar o cliente.
- `RateProvider` está acoplado como instância direta (poderia ser injetado via interface).

## Próximos passos (Fase 4)
- Introduzir `IMoneyConverter` e `IRateProvider`.
- Usar fábrica ou injeção de dependência.
- Adicionar testes unitários (xUnit).

