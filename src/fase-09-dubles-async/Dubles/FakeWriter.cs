using System;
using System.Threading;
using System.Threading.Tasks;
using Fase09.DublesAsync.Contracts;

namespace Fase09.DublesAsync.Dubles;

public sealed class FakeWriter<T> : IAsyncWriter<T>
{
    private readonly int _failTimes;
    private int _attempts = 0;

    public FakeWriter(int failTimes = 0)
    {
        _failTimes = failTimes;
    }

    public Task WriteAsync(T item, CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        if (_attempts < _failTimes)
        {
            _attempts++;
            throw new Exception("Falha simulada em escrita");
        }

        return Task.CompletedTask;
    }
}
