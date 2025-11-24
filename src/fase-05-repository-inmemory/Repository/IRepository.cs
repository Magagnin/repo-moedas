using System.Collections.Generic;

namespace Fase05.RepositoryInMemory.Repository;

public interface IRepository<T, TId>
{
    /// <summary>Adiciona a entidade ao repositório. Retorna a entidade adicionada.</summary>
    T Add(T entity);

    /// <summary>Retorna a entidade pelo id ou null se não existir.</summary>
    T? GetById(TId id);

    /// <summary>Lista todos os itens (IReadOnlyList para não expor coleções mutáveis).</summary>
    IReadOnlyList<T> ListAll();

    /// <summary>Atualiza a entidade. Retorna true se atualizado, false caso não exista.</summary>
    bool Update(T entity);

    /// <summary>Remove pelo id. Retorna true se removido; false caso não exista.</summary>
    bool Remove(TId id);
}
