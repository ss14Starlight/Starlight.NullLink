namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.SttBeginEvent")]
public sealed record SttBeginEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
}

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.SttEndEvent")]
public sealed record SttEndEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }

    [Id(1)]
    public required string Text { get; init; }
}
