namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.AchievementProgressChangedEvent")]
public sealed record AchievementProgressChangedEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
    [Id(1)]
    public required string ProgressType { get; init; }
    [Id(2)]
    public required double Value { get; init; }
}
