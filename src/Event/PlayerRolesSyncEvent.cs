namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.PlayerRolesSyncEvent")]
public sealed record PlayerRolesSyncEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
    [Id(1)]
    public required ulong[] Roles { get; init; }
    [Id(2)]
    public ulong DiscordId { get; init; }
}