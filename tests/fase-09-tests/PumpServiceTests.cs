using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Fase09.DublesAsync.Services;
using Fase09.DublesAsync.Dubles;
using Fase09.DublesAsync.Contracts;

public class PumpServiceTests
{
    [Fact]
    public async Task RunAsync_ShouldProcessAllItems_OnSuccess()
    {
        var reader = new FakeReader<int>(new[] { 1, 2, 3 });
        var writer = new FakeWriter<int>();
        var clock = new FakeClock();

        var service = new PumpService<int>(reader, writer, clock);

        var result = await service.RunAsync();
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task RunAsync_ShouldRetry_OnWriterFailures()
    {
        var reader = new FakeReader<int>(new[] { 1, 2 });
        var writer = new FakeWriter<int>(failTimes: 2);
        var clock = new FakeClock();

        var service = new PumpService<int>(reader, writer, clock);

        var result = await service.RunAsync();
        Assert.Equal(2, result);
    }

    [Fact]
    public async Task RunAsync_ShouldCancel()
    {
        var reader = new FakeReader<int>(new[] { 1, 2, 3 });
        var writer = new FakeWriter<int>();
        var clock = new FakeClock();

        var service = new PumpService<int>(reader, writer, clock);

        using var cts = new CancellationTokenSource();
        cts.CancelAfter(10);

        await Assert.ThrowsAsync<TaskCanceledException>(() => service.RunAsync(cts.Token));
    }

    [Fact]
    public async Task RunAsync_ShouldHandleEmptyStream()
    {
        var reader = new FakeReader<int>(Array.Empty<int>());
        var writer = new FakeWriter<int>();
        var clock = new FakeClock();

        var service = new PumpService<int>(reader, writer, clock);

        var result = await service.RunAsync();
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task RunAsync_ShouldFailOnMiddleStreamError()
    {
        var reader = new FakeReader<int>(new[] { 1, 2, 3 }, throwOnSecond: true);
        var writer = new FakeWriter<int>();
        var clock = new FakeClock();

        var service = new PumpService<int>(reader, writer, clock);

        await Assert.ThrowsAsync<Exception>(() => service.RunAsync());
    }
}
