using System.ComponentModel;
using Orleans.Concurrency;
using Orleans;

namespace Starlight.NullLink.Abstract;

public interface ICrudGrain<T>
{
    ValueTask Clear();
    [Orleans.Concurrency.ReadOnly]
    ValueTask<T> Get();
    ValueTask Set(T value);
}
