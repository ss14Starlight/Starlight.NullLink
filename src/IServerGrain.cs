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

    [Public, Alias("BugReport")]
    public ValueTask BugReport(string player, string title, string description);
    //
    [Public, Alias("SetGhostTheme")]
    public ValueTask SetGhostTheme(Guid player, GhostTheme theme);

    [Public, Alias("UpdateResource")]
    public ValueTask UpdateResource(Guid player, string key, double value);

    // ---- Events ----
    [Public, Alias("ResubscribeEventBus")]
    public ValueTask ResubscribeEventBus(IEventBusObserver observer);
    [Public, Alias("UnsubscribeEventBus")]
    public ValueTask UnsubscribeEventBus(IEventBusObserver observer);

}

[GenerateSerializer]
[Alias("Starlight.NullLink.PlayerData")]
public sealed class PlayerData
{
    [Id(0)]
    public GhostTheme GhostTheme = new();
    [Id(1)]
    public Dictionary<string, double> Resources = [];
    [Id(2)]
    public ulong[] DiscordRoles { get; set; } = [];
}

[GenerateSerializer]
[Alias("Starlight.NullLink.GhostTheme")]
public sealed record GhostTheme
{
    [Id(0)]
    public string Id { get; set; } = "None";

    [Id(1)]
    public Color Color = Color.White;
}
