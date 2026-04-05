namespace Starlight.NullLink;

[GenerateSerializer]
[Alias("AdminUnban")]
public record struct AdminUnban(int banId, Guid? unbanningAdmin, DateTimeOffset unbanTime, string? projectName, string? serverName)
{
    [Id(0)]
    public int BanId { get; } = banId;
    [Id(1)]
    public Guid? UnbanningAdmin { get; } = unbanningAdmin;
    [Id(2)]
    public DateTimeOffset UnbanTime { get; } = unbanTime;
    [Id(3)]
    public string? ProjectName = projectName;
    [Id(4)]
    public string? ServerName = serverName;
}
