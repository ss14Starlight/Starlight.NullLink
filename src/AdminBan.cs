namespace Starlight.NullLink;

[GenerateSerializer]
[Alias("AdminBan")]
public class AdminBan
{
    [Id(0)]
    public int? Id { get; init; }
    [Id(1)]
    public Guid? UserId { get; init; }
    [Id(2)]
    public AddressInfo? Address { get; init; }
    [Id(3)]
    public HwIdInfo? HWId { get; init; }

    [Id(4)]
    public DateTimeOffset BanTime { get; init; }
    [Id(5)]
    public DateTimeOffset? ExpirationTime { get; init; }
    [Id(6)]
    public int? RoundId { get; init; }
    [Id(7)]
    public TimeSpan PlayTimeAtNote { get; init; }
    [Id(8)]
    public string? Reason { get; init; }
    [Id(9)]
    public string? Severity { get; init; }
    [Id(10)]
    public Guid? BanningAdmin { get; init; }
    [Id(11)]
    public List<AdminUnban> Unban { get; init; } = [];
    [Id(12)]
    public string? Role { get; init; }
    [Id(13)]
    public int? ExemptFlags { get; init; }
    [Id(14)]
    public string? ProjectName { get; init; }
    [Id(15)]
    public string? ServerName { get; init; }
}

[GenerateSerializer]
[Alias("AddressInfo")]
public class AddressInfo
{
    [Id(0)]
    public string Address { get; set; } = default!;
    [Id(1)]
    public int CidrMask { get; set; }
}

[GenerateSerializer]
[Alias("HwIdInfo")]
public class HwIdInfo
{
    [Id(0)]
    public byte[] Hwid { get; set; } = default!;
    [Id(1)]
    public int Type { get; set; }
}
