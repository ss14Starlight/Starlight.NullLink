namespace Starlight.NullLink;

// Server A asks Server B to check and prepare the entities.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferPrepareEvent")]
public sealed record EntityTransferPrepareEvent : InterServerEvent
{
    [Id(0)]
    public required EntityTransferPrepareRequest Request { get; set; }
}

// Server B sends this back after processing Prepare.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferPrepareResultEvent")]
public sealed record EntityTransferPrepareResultEvent : InterServerEvent
{
    [Id(0)]
    public required EntityTransferPrepareResult Result { get; set; }
}

// Server A tells Server B to finish the prepared transfer.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferCommitEvent")]
public sealed record EntityTransferCommitEvent : InterServerEvent
{
    [Id(0)]
    public required EntityTransferCommitRequest Request { get; set; }
}

// Server B sends this back after Commit.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferCommitResultEvent")]
public sealed record EntityTransferCommitResultEvent : InterServerEvent
{
    [Id(0)]
    public required EntityTransferCommitResult Result { get; set; }
}

// Server A tells Server B to clean up/cancel.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferAbortEvent")]
public sealed record EntityTransferAbortEvent : InterServerEvent
{
    [Id(0)]
    public required EntityTransferAbortRequest Request { get; set; }
}

// Server B sends this back after Abort.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferAbortResultEvent")]
public sealed record EntityTransferAbortResultEvent : InterServerEvent
{
    [Id(0)]
    public required EntityTransferAbortResult Result { get; set; }
}

// Content-side transfer errors.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferFailureCode")]
public enum EntityTransferFailureCode : byte
{
    None = 0,

    DestinationUnavailable = 1,
    DestinationRejected = 2,
    TimedOut = 3,
    PermissionDenied = 4,
    TransferTooLarge = 5,

    InvalidSaga = 10,
    DuplicateSaga = 11,
    ExpiredSaga = 12,
    ProtocolMismatch = 13,
    InvalidPayload = 14,

    UnknownPrototype = 20,
    UnknownFallbackPrototype = 21,
    ComponentDeserializeFailed = 22,
    ContainerRestoreFailed = 23,
    EntityRestoreFailed = 24,
    ArrivalUnavailable = 25,
    PlayerUnavailable = 26,

    InternalError = 255,
}

// Result of the prepare step.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferPrepareResult")]
public sealed record EntityTransferPrepareResult
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

// Result of the commit step.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferCommitResult")]
public sealed record EntityTransferCommitResult
{
    [Id(0)]
    public bool Accepted { get; set; }

    [Id(1)]
    public EntityTransferFailureCode FailureCode { get; set; }

    [Id(2)]
    public string? Message { get; set; }
}

// Result of the abort step.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferAbortResult")]
public sealed record EntityTransferAbortResult
{
    [Id(0)]
    public bool Accepted { get; set; }

    [Id(1)]
    public EntityTransferFailureCode FailureCode { get; set; }

    [Id(2)]
    public string? Message { get; set; }
}

/// <summary>
/// First step of the saga.
/// Source sends the full batch of entities it wants the destination to prepare.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferPrepareRequest")]
public sealed record EntityTransferPrepareRequest
{
    /// <summary>
    /// Name of the content-side transfer protocol.
    /// </summary>
    [Id(0)]
    public required string Protocol { get; set; }

    /// <summary>
    /// Version of the content-side transfer protocol.
    /// </summary>
    [Id(1)]
    public int ProtocolVersion { get; set; }

    /// <summary>
    /// Optional source hash.
    /// </summary>
    [Id(2)]
    public string? SourceBuildHash { get; set; }

    /// <summary>
    /// Optional content project name.
    /// </summary>
    [Id(3)]
    public string? SourceContentFork { get; set; }

    /// <summary>
    /// Destination-owned arrival point id.
    /// Source asks for this point, but destination should still decide if it is valid.
    /// </summary>
    [Id(4)]
    public string? ArrivalPoint { get; set; }

    /// <summary>
    /// All entities in this transfer batch.
    /// Parent/container ids describe how contained entities fit together.
    /// </summary>
    [Id(5)]
    public EntityTransferEntity[] Entities { get; set; } = [];

    /// <summary>
    /// Which player should be attached to which transferred entity.
    /// </summary>
    [Id(6)]
    public EntityTransferPlayerBinding[] PlayerBindings { get; set; } = [];
}

/// <summary>
/// Commit step.
/// Source sends this after destination has prepared the batch successfully.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferCommitRequest")]
public sealed record EntityTransferCommitRequest
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

/// <summary>
/// Abort step.
/// Source sends this when something failed or timed out.
/// Destination should clean up any prepared entities for the saga.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferAbortRequest")]
public sealed record EntityTransferAbortRequest
{
    [Id(0)]
    public string? Reason { get; set; }
}

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

/// <summary>
/// Tells destination which transferred entity belongs to which player.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferPlayerBinding")]
public sealed record EntityTransferPlayerBinding
{
    [Id(0)]
    public Guid Player { get; set; }

    [Id(1)]
    public Guid MobTransferEntityId { get; set; }
}

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
