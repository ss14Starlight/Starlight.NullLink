namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.PlayerResourcesSyncEvent")]
public sealed record PlayerResourcesSyncEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
    [Id(1)]
    public required Dictionary<string, double> Resources { get; init; }
}