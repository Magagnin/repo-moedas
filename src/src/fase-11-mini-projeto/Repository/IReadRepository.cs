using System.Collections.Generic;

namespace Fase11.MiniProject.Repository;

public interface IReadRepository<T, TId>
{
    T? GetById(TId id);
    IReadOnlyList<T> ListAll();
}
