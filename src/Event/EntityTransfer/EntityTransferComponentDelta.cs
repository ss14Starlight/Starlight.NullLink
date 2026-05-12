namespace Starlight.NullLink.Event.EntityTransfer;

/// <summary>
/// Opaque component data.
/// NullLink does not deserialize this.
/// Source content writes it, destination content reads it.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferComponentDelta")]
public sealed record EntityTransferComponentDelta
{
    /// <summary>
    /// Component name/type id as content understands it.
    /// </summary>
    [Id(0)]
    public required string ComponentName { get; set; }

    /// <summary>
    /// Version of this component's transfer format.
    /// </summary>
    [Id(1)]
    public int Version { get; set; }

    /// <summary>
    /// Serialized component delta.
    /// This should be a delta from the prototype, not a full entity dump.
    /// </summary>
    [Id(2)]
    public byte[] Data { get; set; } = [];
}
