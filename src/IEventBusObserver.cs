using Orleans.Concurrency;
using Starlight.NullLink.Event;

namespace Starlight.NullLink;

[Alias("Starlight.NullLink.IEventBusObserver")]
public interface IEventBusObserver : IGrainObserver
{

    [OneWay]
    [Alias("OnEventReceived<T>")]
    ValueTask OnEventReceived<T>(T @event) where T : BaseEvent;
}