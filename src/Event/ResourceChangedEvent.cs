using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight.NullLink.Event;
public sealed record ResourceChangedEvent : BaseEvent
{
    public required Guid Player { get; init; }
    public required string Resource { get; init; }
    public required double NewAmount { get; init; }
}
