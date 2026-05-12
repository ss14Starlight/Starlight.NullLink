namespace Starlight.NullLink.Event.EntityTransfer;

/// <summary>
/// One entity in a transfer batch.
///
/// This does not use EntityUid because those only mean something on one server.
/// TransferEntityId is just a temp id.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferEntity")]
public sealed record EntityTransferEntity
{
    /// <summary>
    /// Temp id used only inside this saga.
    /// </summary>
    [Id(0)]
    public Guid TransferEntityId { get; set; }

    /// <summary>
    /// Parent entity if this entity was inside another entity's container.
    /// Null means this is a root entity.
    /// </summary>
    [Id(1)]
    public Guid? ParentTransferEntityId { get; set; }

    /// <summary>
    /// Container id on the parent entity.
    /// Backpack storage, belt slot, hand container, etc.
    /// </summary>
    [Id(2)]
    public string? ContainerId { get; set; }

    /// <summary>
    /// Optional stable ordering inside the container.
    /// Content can ignore this if the container does not care about order.
    /// </summary>
    [Id(3)]
    public int? ContainerIndex { get; set; }

    /// <summary>
    /// Original prototype id from the source server.
    /// Destination should try this first.
    /// </summary>
    [Id(4)]
    public required string PrototypeId { get; set; }

    /// <summary>
    /// Fallback prototype id if the real prototype/components do not exist on destination.
    /// This is how cross-version/repo transfers can degrade safely.
    /// </summary>
    [Id(5)]
    public string? FallbackPrototypeId { get; set; }

    /// <summary>
    /// Components that already existed on the original prototype but had changed state.
    /// </summary>
    [Id(6)]
    public EntityTransferComponentDelta[] ComponentDeltas { get; set; } = [];

    /// <summary>
    /// Components that were added after spawn.
    /// If destination does not know one of these, it can use FallbackPrototypeId or reject.
    /// </summary>
    [Id(7)]
    public EntityTransferComponentDelta[] AddedComponents { get; set; } = [];

    /// <summary>
    /// Components that were removed compared to the prototype.
    /// </summary>
    [Id(8)]
    public EntityTransferRemovedComponent[] RemovedComponents { get; set; } = [];
}
