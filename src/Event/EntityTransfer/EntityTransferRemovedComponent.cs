namespace Starlight.NullLink.Event.EntityTransfer;

/// <summary>
/// Component that should be removed from the destination entity after it is spawned.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferRemovedComponent")]
public sealed record EntityTransferRemovedComponent
{
    [Id(0)]
    public required string ComponentName { get; set; }
}
