namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.PlayerAchievementsSyncEvent")]
public sealed record PlayerAchievementsSyncEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
    [Id(1)]
    public required HashSet<Achievement> Achievements { get; init; }
}
