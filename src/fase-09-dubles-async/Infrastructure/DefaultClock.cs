using System;
using System.Threading;
using System.Threading.Tasks;
using Fase09.DublesAsync.Contracts;

namespace Fase09.DublesAsync.Infrastructure;

/// <summary>
/// Implementação real do clock — usa DateTimeOffset.Now e Task.Delay.
/// </summary>
public sealed class DefaultClock : IClock
{
    public DateTimeOffset Now => DateTimeOffset.Now;

    public Task DelayAsync(TimeSpan delay, CancellationToken ct = default) => Task.Delay(delay, ct);
}
