namespace Starlight.NullLink.Event;

public record BaseEvent
{

}

public interface IPlayerEvent
{
    Guid Player { get; }
}