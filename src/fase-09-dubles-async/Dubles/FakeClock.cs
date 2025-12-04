using System;
using Fase09.DublesAsync.Contracts;

namespace Fase09.DublesAsync.Dubles;

public sealed class FakeClock : IClock
{
    public DateTimeOffset Now { get; private set; } = DateTimeOffset.UtcNow;

    public void Advance(TimeSpan ts)
    {
        Now = Now.Add(ts);
    }
}
