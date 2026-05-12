namespace Starlight.NullLink.Event.EntityTransfer;

/// <summary>
/// Result for a single entity in a Prepare response.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferEntityResult")]
public sealed record EntityTransferEntityResult
{
    [Id(0)]
    public Guid TransferEntityId { get; set; }

    [Id(1)]
    public bool Accepted { get; set; }

    [Id(2)]
    public EntityTransferFailureCode FailureCode { get; set; }

    [Id(3)]
    public string? Message { get; set; }

    /// <summary>
    /// The prototype destination actually used.
    /// This may be PrototypeId or FallbackPrototypeId.
    /// </summary>
    [Id(4)]
    public string? RestoredPrototypeId { get; set; }
}
