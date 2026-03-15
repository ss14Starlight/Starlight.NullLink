namespace Starlight.NullLink;

[GenerateSerializer]
[Alias("AdminNote")]
public record struct AdminNote
{
    // Original is id, but we adding project name(key) at the start of id like this: STARLIGHT-123 to separate them between projects. It's not an ChatGPT comment 100%.
    [Id(0)]
    public int Id { get; set; }
    [Id(1)]
    public Guid Player { get; set; }
    [Id(2)]
    public string ProjectName { get; set; }
    [Id(3)]
    public string ServerName { get; set; }
    [Id(4)]
    public int? Round { get; set; }
    [Id(5)]
    public TimeSpan PlaytimeAtNote { get; set; }
    [Id(6)]
    public string NoteType { get; set; }
    [Id(7)]
    public string Message { get; set; }
    [Id(8)]
    public string? NoteSeverity { get; set; }
    [Id(9)]
    public bool Secret { get; set; }
    [Id(10)]
    public string CreatedByName { get; set; }
    [Id(11)]
    public string EditedByName { get; set; }
    [Id(12)]
    public DateTime CreatedAt { get; set; }
    [Id(13)]
    public DateTime? LastEditedAt { get; set; }
    [Id(14)]
    public DateTime? ExpiryTime { get; set; }
    [Id(15)]
    public string[]? BannedRoles { get; set; }
    [Id(16)]
    public DateTime? UnbannedTime { get; set; }
    [Id(17)]
    public string? UnbannedByName { get; set; }
    [Id(18)]
    public bool? Seen { get; set; }
}