
# Fase 0 — Casos escolhidos (Conversão de Moedas)

## Caso 1 — Conversão BRL → Moeda Estrangeira (simples)
1. **Objetivo:** permitir que o usuário converta um valor em reais (BRL) para uma moeda estrangeira (USD, EUR, GBP, JPY, ...).
2. **Contrato:** `ConverterBRL(valorBRL, codigoMoeda, politica)` -> retorna (valorConvertido, cotacaoUsada, politicaUsada) ou erro.
3. **Implementação A:** Taxas fixas (cotação pré-definida no sistema). Risco: desatualizado.
4. **Implementação B:** Taxas configuráveis pelo usuário no início da sessão. Risco: usuário erra taxa.
5. **Política de escolha:** 
   - Se `modo = "Automático"` usar fonte fixa (configuração do sistema).
   - Se `modo = "Personalizado"` usar taxa fornecida pelo usuário.
   - Se `modo = "Comercial/Turismo"` aplicar markup (veja Caso 2).
6. **Riscos/Observações:** Flutuação de mercado; arredondamento; taxas (spread) aplicadas por bancos.

## Caso 2 — Conversão entre moedas estrangeiras e políticas
1. **Objetivo:** converter entre duas moedas estrangeiras (ex.: USD → EUR) de forma precisa.
2. **Contrato:** `ConverterDireta(valor, de, para, politica)` -> valor convertido.
3. **Implementação A:** Converter via BRL (de → BRL → para) usando cotação BRL↔moedas.
4. **Implementação B:** Converter usando cotação direta (se disponível).
5. **Política de escolha:** 
   - Preferir cotação direta se disponível; se não, usar via BRL.
   - Aplicar política de spread: `comercial` (menor spread), `turismo` (maior spread).
6. **Riscos/Observações:** Diferença entre cotações de compra/venda; precisão decimal; taxas de serviço.

## Requisitos não-funcionais
- Arredondamento configurável (2 casas ou 4 casas).
- Logs básicos das conversões (console).
- Interface de console simples (menu).

