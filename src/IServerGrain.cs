﻿using Orleans;
using Starlight.NullLink.Abstract;
using Starlight.NullLink.Attributes;

namespace Starlight.NullLink;

[Alias("Starlight.NullLink.IServerGrain")]
public interface IServerGrain : IGrainWithStringKey
{
    // ---- Hub ----
    [Public, Alias("UpdateServer")]
    public ValueTask UpdateServer(Server server);

    [Public, Alias("UpdateServerInfo")]
    public ValueTask UpdateServerInfo(ServerInfo info);

    // ---- Player ---- 
    [Public, Alias("GetPlayerData")]
    public ValueTask<PlayerData> GetPlayerData(Guid player);

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
