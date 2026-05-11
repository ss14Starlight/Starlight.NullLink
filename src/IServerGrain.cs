using Orleans;
using Starlight.NullLink.Abstract;
using Starlight.NullLink.Attributes;
using System.Collections.Immutable;
using System.Net;

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

    [Public, Alias("GetPlayTime")]
    public ValueTask<PlayTime[]> GetPlayTime(Guid player, string[] trackers, string[] recognition);

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
    public ValueTask<HashSet<AdminNote>> RequestNotes(Guid player);

    [Public, Alias("AddNoteOrUpdate")]
    public ValueTask AddOrUpdateNote(AdminNote note, string? project = null, Guid? player = null);

    [Public, Alias("RemoveNote")]
    public ValueTask RemoveNote(Guid player, int id, string? project = null, Guid? removedBy = null);

    // ---- Bans ----

    [Public, Alias("RequestBanById")]
    public ValueTask<AdminBan?> RequestBanById(int id, string? project = null, string? server = null);

    [Public, Alias("RequestBan")]
    public ValueTask<AdminBan?> RequestBan(IPAddress? address, Guid? userId, ImmutableArray<byte>? hwId, ImmutableArray<ImmutableArray<byte>>? modernHWIds);

    [Public, Alias("RequestBans")]
    public ValueTask<List<AdminBan>> RequestBans(Guid? player, IPAddress? Address, ImmutableArray<byte>? hwId, ImmutableArray<ImmutableArray<byte>>? modernHWIds, bool includeUnbanned = true, bool role = false);

    [Public, Alias("AddOrUpdateBan")]
    public ValueTask AddOrUpdateBan(AdminBan note);

    // ---- Events ----
    [Public, Alias("ResubscribeEventBus")]
    public ValueTask ResubscribeEventBus(IEventBusObserver observer);
    [Public, Alias("UnsubscribeEventBus")]
    public ValueTask UnsubscribeEventBus(IEventBusObserver observer);

    // ---- Inter-server Messages ----
    /// <summary>
    /// Sends an event to another server.
    /// </summary>
    [Public, Alias("SendMessageToServer")]
    public ValueTask<InterServerMessageResult> SendMessageToServer(string destinationServer, InterServerEvent message);

    // ---- Achievments ----
    [Public, Alias("GetAllUnlockedAchievements")]
    public ValueTask<HashSet<Achievement>> GetUnlockedAchievements(Guid player);
    [Public, Alias("HasAchievementUnlocked")]
    public ValueTask<bool> HasAchievementUnlocked(Guid player, string achievementId);
    [Public, Alias("UnlockAchievement")]
    public ValueTask UnlockAchievement(Guid player, string achievementId, string characterName);
    [Public, Alias("LockAchievement")]
    public ValueTask LockAchievement(Guid player, string achievementId);
    [Public, Alias("GetAchievementProgress")]
    public ValueTask<Dictionary<string, double>> GetAchievementProgress(Guid player);
    [Public, Alias("SetAchievementProgress")]
    public ValueTask SetAchievementProgress(Guid player, string key, double value);
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
    [Id(4)]
    public Dictionary<string, double> AchievementProgress { get; set; } = [];
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
