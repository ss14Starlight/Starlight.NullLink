using System.Collections.Immutable;
using System.Net;

namespace Starlight.NullLink;

[GenerateSerializer]
[Alias("AdminBan")]
public record struct AdminBan
{
    public AdminBan(int? id, Guid? userId, (IPAddress, int)? address, (ImmutableArray<byte> hwid, int type)? hwId, DateTimeOffset banTime, DateTimeOffset? expirationTime, int? roundId, TimeSpan playtimeAtNote, string reason, string severity, Guid? banningAdmin, AdminUnban? unban, string? role, int? exemptFlags)
    {
        if (userId == null && address == null && hwId == null)
            throw new ArgumentException("Why are you trying to create role ban with zero information about user?");

        if (role == null && exemptFlags == null)
            throw new ArgumentException("Why are you trying to create role ban with zero information about ban?");

        Id = id;
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
    }
    [Id(0)]
    public int? Id { get; }
    [Id(1)]
    public Guid? UserId { get; }
    [Id(2)]
    public (IPAddress address, int cidrMask)? Address { get; }
    [Id(3)]
    public (ImmutableArray<byte> hwid, int type)? HWId { get; }

    [Id(4)]
    public DateTimeOffset BanTime { get; }
    [Id(5)]
    public DateTimeOffset? ExpirationTime { get; }
    [Id(6)]
    public int? RoundId { get; }
    [Id(7)]
    public TimeSpan PlayTimeAtNote { get; }
    [Id(8)]
    public string Reason { get; }
    [Id(9)]
    public string Severity { get; }
    [Id(10)]
    public Guid? BanningAdmin { get; }
    [Id(11)]
    public AdminUnban? Unban { get; }
    [Id(12)]
    public string? Role { get; }
    [Id(13)]
    public int? ExemptFlags { get; }
}
