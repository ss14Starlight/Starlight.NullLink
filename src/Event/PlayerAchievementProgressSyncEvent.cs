namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.PlayerAchievementProgressSyncEvent")]
public sealed record PlayerAchievementProgressSyncEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
    [Id(1)]
    public required Dictionary<string, double> Progress { get; init; }
}
