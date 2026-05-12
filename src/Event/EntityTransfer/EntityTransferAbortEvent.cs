namespace Starlight.NullLink.Event.EntityTransfer;

// Server A tells Server B to clean up/cancel.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferAbortEvent")]
public sealed record EntityTransferAbortEvent : InterServerEvent
{
    [Id(0)]
    public string? Reason { get; set; }
}
