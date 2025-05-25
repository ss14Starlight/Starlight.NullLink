using Orleans.Concurrency;
using Orleans;

namespace Starlight.NullLink.Abstract;

public interface IObservableListCrudGrain<T> : ICrudGrain<List<T>>
{
    public ValueTask<List<T>> GetAndSubscribe(IObserverListCrudGrain<T> observer);
    public ValueTask Resubscribe(IObserverListCrudGrain<T> observer);
    public ValueTask Unsubscribe(IObserverListCrudGrain<T> observer);
    public ValueTask Add(T value);
    public ValueTask Remove(T value);
}

[Alias("IObserverListCrudGrain")]
public interface IObserverListCrudGrain<T> : IGrainObserver
{
    [OneWay]
    [Alias("Add")]
    Task Add(T? value);
    [OneWay]
    [Alias("Remove")]
    Task Remove(T? value);
}
