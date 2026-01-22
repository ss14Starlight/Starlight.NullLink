using Starlight.NullLink;

namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.PlayerNotesSyncEvent")]
public sealed record PlayerNotesSyncEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
    [Id(1)]
    public required List<AdminNote> Notes { get; init; }
}