using Starlight.NullLink.Event;

namespace Starlight.NullLink;

[GenerateSerializer]
[Alias("Starlight.NullLink.InterServerEvent")]
public abstract class InterServerEvent : BaseEvent
{
    [Id(0)]
    public Guid EventId { get; set; } = Guid.NewGuid();

    [Id(1)]
    public Guid? SagaId { get; set; }

    // PROJECT.server
    [Id(2)]
    public required string SourceServer { get; set; }

    // PROJECT.server
    [Id(3)]
    public required string DestinationServer { get; set; }

    [Id(4)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Id(5)]
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(1);
}

// This only means NullLink queued/delivered it.
// It doesn't mean the other server processed it.
[GenerateSerializer]
[Alias("Starlight.NullLink.InterServerMessageResult")]
public sealed class InterServerMessageResult
{
    [Id(0)]
    public bool Accepted { get; set; }

    [Id(1)]
    public InterServerMessageError Error { get; set; }

    [Id(2)]
    public string? Message { get; set; }
}

// Delivery errors.
[GenerateSerializer]
[Alias("Starlight.NullLink.InterServerMessageError")]
public enum InterServerMessageError : byte
{
    None = 0,

    DestinationUnavailable = 1,
    DestinationQueueUnavailable = 2,
    InvalidDestination = 3,
    Expired = 4,
    PermissionDenied = 5,
    PayloadTooLarge = 6,

    InternalError = 255,
}
