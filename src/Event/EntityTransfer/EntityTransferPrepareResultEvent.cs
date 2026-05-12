namespace Starlight.NullLink.Event.EntityTransfer;

// Server B sends this back after processing Prepare.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferPrepareResultEvent")]
public sealed record EntityTransferPrepareResultEvent : InterServerEvent
{
    [Id(0)]
    public bool Accepted { get; set; }

    [Id(1)]
    public EntityTransferFailureCode FailureCode { get; set; }

    [Id(2)]
    public string? Message { get; set; }

    // Per-entity info so the source can tell what failed.
    [Id(3)]
    public EntityTransferEntityResult[] EntityResults { get; set; } = [];

    // The arrival point the destination actually chose.
    [Id(4)]
    public string? ResolvedArrivalPoint { get; set; }
}
