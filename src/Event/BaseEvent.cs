namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.BaseEvent")]
public record BaseEvent
{

}

public interface IPlayerEvent
{
    Guid Player { get; }
}