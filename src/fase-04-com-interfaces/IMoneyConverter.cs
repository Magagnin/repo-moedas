public interface IMoneyConverter
{
    /// <summary>
    /// Converte amount na moeda 'from' para 'to' usando a política informada.
    /// Retorna o valor convertido (decimal bruto). Não faz formatação nem arredondamento final.
    /// </summary>
    decimal Convert(decimal amount, string from, string to, string policy);
}

