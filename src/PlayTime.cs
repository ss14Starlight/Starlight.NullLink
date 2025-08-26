namespace Starlight.NullLink;

[GenerateSerializer]
[Alias("PlayTime")]
public record struct PlayTime
{
    [Id(0)]
    public required string Tracker { get; set; }
    [Id(1)]
    public required TimeSpan Time { get; set; }
}

[GenerateSerializer]
[Alias("PlayerPlayTime")]
public sealed record PlayerPlayTime
{
    [Id(0)]
    public required Guid Player { get; set; }
    [Id(1)]
    public required PlayTime[] PlayTimes { get; set; }
}