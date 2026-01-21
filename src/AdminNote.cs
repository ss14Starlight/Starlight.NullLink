namespace Starlight.NullLink;

[GenerateSerializer]
public sealed record AdminNote(
    int Id,
    Guid Player,
    int? Round,
    string? ServerName,
    TimeSpan PlaytimeAtNote,
    string NoteType,
    string Message,
    string? NoteSeverity,
    bool Secret,
    string CreatedByName,
    string EditedByName,
    DateTime CreatedAt,
    DateTime? LastEditedAt,
    DateTime? ExpiryTime,
    string[]? BannedRoles,
    DateTime? UnbannedTime,
    string? UnbannedByName,
    bool? Seen
    );