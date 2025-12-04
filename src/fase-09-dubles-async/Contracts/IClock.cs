namespace Fase09.DublesAsync.Contracts;

public interface IClock
{
    DateTimeOffset Now { get; }
}
