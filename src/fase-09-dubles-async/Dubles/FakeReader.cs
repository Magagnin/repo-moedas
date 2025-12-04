using System.Collections.Generic;
using System.Threading;
using Fase09.DublesAsync.Contracts;

namespace Fase09.DublesAsync.Dubles;

public sealed class FakeReader<T> : IAsyncReader<T>
{
    private readonly IEnumerable<T> _items;
    private readonly bool _throwOnSecond;

    public FakeReader(IEnumerable<T> items, bool throwOnSecond = false)
    {
        _items = items;
        _throwOnSecond = throwOnSecond;
    }

    public async IAsyncEnumerable<T> ReadAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        int index = 0;

        foreach (var item in _items)
        {
            ct.ThrowIfCancellationRequested();

            if (_throwOnSecond && index == 1)
                throw new Exception("Erro no meio do stream");

            yield return item;
            index++;
            await Task.Yield();
        }
    }
}
