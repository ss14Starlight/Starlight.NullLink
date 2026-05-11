namespace Starlight.NullLink;

/// <summary>
/// What step of the transfer saga this message is for.
/// NullLink should not care about the entity data itself, it only relays the message.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferMessageKind")]
public enum EntityTransferMessageKind : byte
{
    /// <summary>
    /// Source server is asking the destination server if it can prepare the entities.
    /// </summary>
    Prepare = 0,

    /// <summary>
    /// Source server has accepted the destination prepare step.
    /// </summary>
    Commit = 1,

    /// <summary>
    /// Source server is cancelling the transfer.
    /// </summary>
    Abort = 2,
}

/// <summary>
/// Error codes.
/// </summary>
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

/// <summary>
/// Message sent from one server to another for an entity transfer saga.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferMessage")]
public sealed class EntityTransferMessage
{
    /// <summary>
    /// Unique id for this transfer attempt.
    /// Both servers use this to track/rollback the same saga.
    /// </summary>
    [Id(0)]
    public Guid SagaId { get; set; }

    /// <summary>
    /// Which saga step this message is for.
    /// </summary>
    [Id(1)]
    public EntityTransferMessageKind Kind { get; set; }

    /// <summary>
    /// Server that started the transfer.
    /// </summary>
    [Id(2)]
    public required string SourceServer { get; set; }

    /// <summary>
    /// Server that should receive the transfer.
    /// </summary>
    [Id(3)]
    public required string DestinationServer { get; set; }

    /// <summary>
    /// When the source created this message.
    /// </summary>
    [Id(4)]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// When this saga should be considered dead.
    /// Content should rollback if this expires.
    /// </summary>
    [Id(5)]
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Data for the prepare step.
    /// </summary>
    [Id(6)]
    public EntityTransferPrepareRequest? Prepare { get; set; }

    /// <summary>
    /// Data for the commit step.
    /// </summary>
    [Id(7)]
    public EntityTransferCommitRequest? Commit { get; set; }

    /// <summary>
    /// Data for the abort step.
    /// </summary>
    [Id(8)]
    public EntityTransferAbortRequest? Abort { get; set; }
}

/// <summary>
/// Result returned by NullLink after it relays the message.
///
/// Success means the message reached the destination server and it answered.
/// It does not always mean the transfer itself was accepted,
/// Check Response.Accepted for that.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferRelayResult")]
public sealed class EntityTransferRelayResult
{
    [Id(0)]
    public bool Success { get; set; }

    [Id(1)]
    public EntityTransferFailureCode FailureCode { get; set; }

    [Id(2)]
    public string? Message { get; set; }

    /// <summary>
    /// Response from the destination server.
    /// </summary>
    [Id(3)]
    public EntityTransferResponse? Response { get; set; }
}

/// <summary>
/// Response from the destination server for any saga step.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferResponse")]
public sealed class EntityTransferResponse
{
    /// <summary>
    /// True if the destination accepted this saga step.
    /// For Prepare, this means entities were prepared in destination.
    /// For Commit, this means entities were handed over.
    /// For Abort, this means prepared entities were cleaned up.
    /// </summary>
    [Id(0)]
    public bool Accepted { get; set; }

    [Id(1)]
    public EntityTransferFailureCode FailureCode { get; set; }

    [Id(2)]
    public string? Message { get; set; }

    /// <summary>
    /// Per-entity results for Prepare.
    /// Sp the source can know exactly what failed.
    /// </summary>
    [Id(3)]
    public EntityTransferEntityResult[] EntityResults { get; set; } = [];

    /// <summary>
    /// Destination-owned arrival point that was actually accepted.
    /// This can be different from the requested point if the destination used a fallback.
    /// </summary>
    [Id(4)]
    public string? ResolvedArrivalPoint { get; set; }
}

/// <summary>
/// First step of the saga.
/// Source sends the full batch of entities it wants the destination to prepare.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferPrepareRequest")]
public sealed class EntityTransferPrepareRequest
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
public sealed class EntityTransferCommitRequest
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
public sealed class EntityTransferAbortRequest
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
public sealed class EntityTransferEntity
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
public sealed class EntityTransferComponentDelta
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
public sealed class EntityTransferRemovedComponent
{
    [Id(0)]
    public required string ComponentName { get; set; }
}

/// <summary>
/// Tells destination which transferred entity belongs to which player.
/// </summary>
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferPlayerBinding")]
public sealed class EntityTransferPlayerBinding
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
public sealed class EntityTransferEntityResult
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