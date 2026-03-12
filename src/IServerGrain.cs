using Orleans;
using Starlight.NullLink.Abstract;
using Starlight.NullLink.Attributes;

namespace Starlight.NullLink;

/// <summary>
/// Server gateway
/// </summary>
[Alias("Starlight.NullLink.IServerGrain")]
public interface IServerGrain : IGrainWithStringKey
{
    // ---- Hub ----
    [Public, Alias("UpdateServer")]
    public ValueTask UpdateServer(Server server);

    [Public, Alias("UpdateServerInfo")]
    public ValueTask UpdateServerInfo(ServerInfo info);

    // ---- Player ---- 

    [Public, Alias("PlayerConnected")]
    public ValueTask PlayerConnected(Guid player);

    [Public, Alias("PlayerDisconnected")]
    public ValueTask PlayerDisconnected(Guid player);

    // ---- Playtime ----

    [Public, Alias("UpdatePlayersPlayTime")]
    public ValueTask UpdatePlayersPlayTime(PlayerPlayTime[] playerPlayTimes);

    // 

    [Public, Alias("HasPlayerAnyRole")]
    public ValueTask<bool> HasPlayerAnyRole(Guid player, ulong[] roles);

    [Public, Alias("GetPlayerDiscordId")]
    public ValueTask<ulong> GetPlayerDiscordId(Guid player);

    // ---- Bug Reports ----

    [Public, Alias("BugReport")]
    public ValueTask BugReport(string player, string title, string description, Dictionary<string, string> metadata);

    // ---- Resources ----

    /// <summary>
    /// Changes Resource with ID by adding Value. So you can increase or decrease resource value.
    /// </summary>
    [Public, Alias("UpdateResource")]
    public ValueTask UpdateResource(Guid player, string key, double value);

    // ---- Notes ----

    [Public, Alias("RequestNotes")]
    public ValueTask<List<AdminNote>?> RequestNotes(Guid player);

    [Public, Alias("AddNoteOrUpdate")]
    public ValueTask AddOrUpdateNote(Guid player, AdminNote note);

    [Public, Alias("RemoveNote")]
    public ValueTask RemoveNote(Guid player, string id);

    // ---- Events ----
    [Public, Alias("ResubscribeEventBus")]
    public ValueTask ResubscribeEventBus(IEventBusObserver observer);
    [Public, Alias("UnsubscribeEventBus")]
    public ValueTask UnsubscribeEventBus(IEventBusObserver observer);
    
    // ---- Achievments ----
    [Public, Alias("GetAllUnlockedAchievements")]
    public ValueTask<HashSet<Achievement>> GetUnlockedAchievements(Guid player);
    [Public, Alias("HasAchievementUnlocked")]
    public ValueTask<bool> HasAchievementUnlocked(Guid player, string achievementId);
    [Public, Alias("UnlockAchievement")]
    public ValueTask UnlockAchievement(Guid player, string achievementId, string characterName);
    [Public, Alias("LockAchievement")]
    public ValueTask LockAchievement(Guid player, string achievementId);
}

[GenerateSerializer]
[Alias("Starlight.NullLink.PlayerData")]
public sealed class PlayerData
{
    [Id(1)]
    public Dictionary<string, double> Resources = [];
    [Id(2)]
    public ulong[] DiscordRoles { get; set; } = [];
    [Id(3)]
    public HashSet<Achievement> UnlockedAchievements { get; set; } = [];
}

[GenerateSerializer]
[Alias("Starlight.NullLink.Achievement")]
public sealed class Achievement
{
    [Id(1)]
    public required string AchievementId { get; set; } = "";
    [Id(2)]
    public required string GrantingServer { get; set; } = "";
    [Id(3)]
    public string UnlockingCharacter { get; set; } = "";
    [Id(4)]
    public DateTime UnlockTime { get; set; }
}
