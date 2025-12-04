using System;
using System.Threading;
using System.Threading.Tasks;
using Fase09.DublesAsync.Contracts;

namespace Fase09.DublesAsync.Services;

public sealed class PumpService<T>
{
    private readonly IAsyncReader<T> _reader;
    private readonly IAsyncWriter<T> _writer;
    private readonly IClock _clock;

    public PumpService(IAsyncReader<T> reader, IAsyncWriter<T> writer, IClock clock)
        => (_reader, _writer, _clock) = (reader, writer, clock);

    public async Task<int> RunAsync(CancellationToken ct = default)
    {
        var count = 0;

        await foreach (var item in _reader.ReadAsync(ct).WithCancellation(ct))
        {
            var attempt = 0;

            while (true)
            {
                ct.ThrowIfCancellationRequested();
                try
                {
                    await _writer.WriteAsync(item, ct);
                    count++;
                    break;
                }
                catch when (++attempt <= 3)
                {
                    // backoff controlado pelo relógio fake
                    var now = _clock.Now;
                }
            }
        }

        return count;
    }
}
