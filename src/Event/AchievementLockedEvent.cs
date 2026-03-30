namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.AchievementLockedEvent")]
public sealed record AchievementLockedEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
    [Id(1)]
    public required string AchievementId { get; init; }
}
