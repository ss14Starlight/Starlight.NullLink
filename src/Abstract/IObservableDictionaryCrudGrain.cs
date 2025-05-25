using Orleans.Concurrency;
using Orleans;

namespace Starlight.NullLink.Abstract;

public interface IObservableDictionaryCrudGrain<T>
{
    public ValueTask<Dictionary<string, T>> GetAndSubscribe(IObserverDictionaryCrudGrain<T> observer);
    public ValueTask Resubscribe(IObserverDictionaryCrudGrain<T> observer);
    public ValueTask Unsubscribe(IObserverDictionaryCrudGrain<T> observer);
    public ValueTask Add(string key, T value);
    public ValueTask Update(string key, T value);
    public ValueTask Remove(string key);
}

[Alias("IObserverDictionaryCrudGrain")]
public interface IObserverDictionaryCrudGrain<T> : IGrainObserver
{
    [OneWay]
    [Alias("Updated")]
    Task Updated(string key, T value);
    [OneWay]
    [Alias("Remove")]
    Task Remove(string key);
}
