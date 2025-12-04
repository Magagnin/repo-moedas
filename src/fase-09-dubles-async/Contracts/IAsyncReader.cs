using System.Collections.Generic;
using System.Threading;

namespace Fase09.DublesAsync.Contracts;

public interface IAsyncReader<T>
{
    IAsyncEnumerable<T> ReadAsync(CancellationToken ct = default);
}
