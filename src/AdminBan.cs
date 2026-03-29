using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Starlight.NullLink;

[GenerateSerializer]
[Alias("AdminBan")]
[BsonIgnoreExtraElements]
public class AdminBan
{
    [BsonConstructor]
    public AdminBan(int? id, Guid? userId, AddressInfo? address, HwIdInfo? hwId, DateTimeOffset banTime, DateTimeOffset? expirationTime, int? roundId, TimeSpan playtimeAtNote, string reason, string severity, Guid? banningAdmin, List<AdminUnban> unban, string? role, int? exemptFlags, string? projectName, string? serverName)
    {
        if (userId == null && address == null && hwId == null)
            throw new ArgumentException("Why are you trying to create role ban with zero information about user?");

        if (role == null && exemptFlags == null)
            throw new ArgumentException("Why are you trying to create role ban with zero information about ban?");

        Id = id ;
        UserId = userId;
        Address = address;
        HWId = hwId;
        BanTime = banTime;
        ExpirationTime = expirationTime;
        RoundId = roundId;
        PlayTimeAtNote = playtimeAtNote;
        Reason = reason;
        Severity = severity;
        BanningAdmin = banningAdmin;
        Unban = unban;
        Role = role;
        ExemptFlags = exemptFlags;
        ProjectName = projectName;
        ServerName = serverName;
    }
    [Id(0)]
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int? Id { get; }
    [Id(1)]
    [BsonElement("UserId")]
    public Guid? UserId { get; }
    [Id(2)]
    [BsonElement("Address")]
    public AddressInfo? Address { get; }
    [Id(3)]
    [BsonElement("HWId")]
    public HwIdInfo? HWId { get; }

    [Id(4)]
    [BsonElement("BanTime")]
    public DateTimeOffset BanTime { get; }
    [Id(5)]
    [BsonElement("ExpirationTime")]
    public DateTimeOffset? ExpirationTime { get; }
    [Id(6)]
    [BsonElement("RoundId")]
    public int? RoundId { get; }
    [Id(7)]
    [BsonElement("PlayTimeAtNote")]
    public TimeSpan PlayTimeAtNote { get; }
    [Id(8)]
    [BsonElement("Reason")]
    public string Reason { get; }
    [Id(9)]
    [BsonElement("Severity")]
    public string Severity { get; }
    [Id(10)]
    [BsonElement("BanningAdmin")]
    public Guid? BanningAdmin { get; }
    [Id(11)]
    [BsonElement("Unban")]
    public List<AdminUnban> Unban { get; }
    [Id(12)]
    [BsonElement("Role")]
    public string? Role { get; }
    [Id(13)]
    [BsonElement("ExemptFlags")]
    public int? ExemptFlags { get; }
    [Id(14)]
    [BsonElement("ProjectName")]
    public string? ProjectName { get; set; }
    [Id(15)]
    [BsonElement("ServerName")]
    public string? ServerName { get; set; }
}
public class AddressInfo
{
    public string Address { get; set; } = default!;
    public int CidrMask { get; set; }
}
public class HwIdInfo
{
    public byte[] Hwid { get; set; } = default!;
    public int Type { get; set; }
}