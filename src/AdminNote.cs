namespace Starlight.NullLink;

[GenerateSerializer]
[Alias("AdminNote")]
public record struct AdminNote
{
    [Id(0)]
    int Id { get; set; }
    [Id(1)]
    Guid Player { get; set; }
    [Id(2)]
    string ProjectName { get; set; }
    [Id(3)]
    string ServerName { get; set; }
    [Id(4)]
    int? Round { get; set; }
    [Id(5)]
    TimeSpan PlaytimeAtNote { get; set; }
    [Id(6)]
    string NoteType { get; set; }
    [Id(7)]
    string Message { get; set; }
    [Id(8)]
    string? NoteSeverity { get; set; }
    [Id(9)]
    bool Secret { get; set; }
    [Id(10)]
    string CreatedByName { get; set; }
    [Id(11)]
    string EditedByName { get; set; }
    [Id(12)]
    DateTime CreatedAt { get; set; }
    [Id(13)]
    DateTime? LastEditedAt { get; set; }
    [Id(14)]
    DateTime? ExpiryTime { get; set; }
    [Id(15)]
    string[]? BannedRoles { get; set; }
    [Id(16)]
    DateTime? UnbannedTime { get; set; }
    [Id(17)]
    string? UnbannedByName { get; set; }
    [Id(18)]
    bool? Seen { get; set; }
}