using Starlight.NullLink;

namespace Starlight.NullLink.Event;

[GenerateSerializer]
[Alias("Starlight.NullLink.Event.NotesChangedEvent")]
public sealed record NotesChangedEvent : BaseEvent, IPlayerEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
    [Id(1)]
    public required List<AdminNote> Add { get; init; }
    [Id(2)]
    public required List<AdminNote> Update { get; init; }
    [Id(3)]
    public required List<AdminNote> Remove { get; init; }
}
