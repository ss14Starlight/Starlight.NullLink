namespace Starlight.NullLink.Event.EntityTransfer;

// Server A asks Server B to check and prepare the entities.
[GenerateSerializer]
[Alias("Starlight.NullLink.EntityTransferPrepareEvent")]
public sealed record EntityTransferPrepareEvent : InterServerEvent
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
