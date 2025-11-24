using System.Collections.Generic;

namespace Fase08.Isp.Repository;

/// <summary>
/// Contrato mínimo de leitura (ISP).
/// </summary>
public interface IReadRepository<T, TId>
{
    T? GetById(TId id);
    IReadOnlyList<T> ListAll();
}
