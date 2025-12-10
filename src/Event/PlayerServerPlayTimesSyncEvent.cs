namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.PlayerServerPlayTimesSyncEvent")]
public sealed record PlayerServerPlayTimesSyncEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
    [Id(1)]
    public required Dictionary<string, PlayTime[]> ServerPlayTimes { get; init; }
}