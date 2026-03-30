namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.AchievementUnlockedEvent")]
public sealed record AchievementUnlockedEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
    [Id(1)]
    public required Achievement Achievement { get; init; }
}
