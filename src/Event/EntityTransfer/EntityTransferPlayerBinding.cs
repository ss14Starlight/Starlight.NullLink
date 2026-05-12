namespace Starlight.NullLink.Event.EntityTransfer;

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
