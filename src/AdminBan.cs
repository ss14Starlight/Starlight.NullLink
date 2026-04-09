namespace Starlight.NullLink;

[GenerateSerializer]
[Alias("AdminBan")]
public class AdminBan
{
    [Id(0)]
    public int? Id { get; }
    [Id(1)]
    public Guid? UserId { get; }
    [Id(2)]
    public AddressInfo? Address { get; }
    [Id(3)]
    public HwIdInfo? HWId { get; }

    [Id(4)]
    public DateTimeOffset BanTime { get; }
    [Id(5)]
    public DateTimeOffset? ExpirationTime { get; }
    [Id(6)]
    public int? RoundId { get; }
    [Id(7)]
    public TimeSpan PlayTimeAtNote { get; }
    [Id(8)]
    public string? Reason { get; }
    [Id(9)]
    public string? Severity { get; }
    [Id(10)]
    public Guid? BanningAdmin { get; }
    [Id(11)]
    public List<AdminUnban> Unban { get; } = [];
    [Id(12)]
    public string? Role { get; }
    [Id(13)]
    public int? ExemptFlags { get; }
    [Id(14)]
    public string? ProjectName { get; set; }
    [Id(15)]
    public string? ServerName { get; set; }
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
