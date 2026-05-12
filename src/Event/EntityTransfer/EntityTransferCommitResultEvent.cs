namespace Starlight.NullLink.Event.EntityTransfer;

// Server B sends this back after Commit.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferCommitResultEvent")]
public sealed record EntityTransferCommitResultEvent : InterServerEvent
{
    [Id(0)]
    public bool Accepted { get; set; }

    [Id(1)]
    public EntityTransferFailureCode FailureCode { get; set; }

    [Id(2)]
    public string? Message { get; set; }
}
