namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.RolesChangedEvent")]
public sealed record RolesChangedEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
    [Id(1)]
    public required ulong[] Add { get; init; }
    [Id(2)]
    public required ulong[] Remove { get; init; }
    [Id(3)]
    public ulong DiscordId { get; init; }
}
