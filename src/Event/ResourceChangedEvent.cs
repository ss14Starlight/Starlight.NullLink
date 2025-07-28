using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight.NullLink.Event;
[GenerateSerializer]
[Alias("Starlight.NullLink.Event.ResourceChangedEvent")]
public sealed record ResourceChangedEvent : BaseEvent
{
    [Id(0)]
    public required Guid Player { get; init; }
    [Id(1)]
    public required string Resource { get; init; }
    [Id(2)]
    public required double NewAmount { get; init; }
}
