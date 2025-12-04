using System.Threading;
using System.Threading.Tasks;

namespace Fase09.DublesAsync.Contracts;

public interface IAsyncWriter<T>
{
    Task WriteAsync(T item, CancellationToken ct = default);
}
