namespace Starlight.NullLink.Event.EntityTransfer;

// Server A tells Server B to finish the prepared transfer.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferCommitEvent")]
public sealed record EntityTransferCommitEvent : InterServerEvent
{
    /// <summary>
    /// Destination arrival point id to use when finalizing the transfer.
    /// </summary>
    [Id(0)]
    public string? ArrivalPoint { get; set; }

    /// <summary>
    /// Players that should now be attached to their prepared mobs.
    /// </summary>
    [Id(1)]
    public Guid[] PlayersToAttach { get; set; } = [];
}
