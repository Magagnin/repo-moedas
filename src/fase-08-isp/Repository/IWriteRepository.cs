namespace Fase08.Isp.Repository;

/// <summary>
/// Contrato mínimo de escrita (ISP).
/// </summary>
public interface IWriteRepository<T, TId>
{
    T Add(T entity);
    bool Update(T entity);
    bool Remove(TId id);
}
