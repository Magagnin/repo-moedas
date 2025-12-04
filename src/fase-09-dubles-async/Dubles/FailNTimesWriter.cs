using System;
using System.Threading;
using System.Threading.Tasks;
using Fase09.DublesAsync.Contracts;

namespace Fase09.DublesAsync.Fakes;

/// <summary>
/// Writer that fails N times before succeeding. Useful to test retry logic.
/// </summary>
public sealed class FailNTimesWriter<T> : IAsyncWriter<T>
{
    private int _failRemaining;
    private readonly IAsyncWriter<T>? _inner;

    /// <param name="failCount">number of times to throw before delegating to inner (or succeeding)</param>
    /// <param name="inner">optional writer to call after failures; if null, simply returns completed Task on success</param>
    public FailNTimesWriter(int failCount, IAsyncWriter<T>? inner = null)
    {
        _failRemaining = Math.Max(0, failCount);
        _inner = inner;
    }

    public Task WriteAsync(T item, CancellationToken ct = default)
    {
        if (_failRemaining > 0)
        {
            _failRemaining--;
            throw new InvalidOperationException("Simulated writer failure");
        }
        return _inner?.WriteAsync(item, ct) ?? Task.CompletedTask;
    }
}

