using Orleans;
using Starlight.NullLink.Attributes;

namespace Starlight.NullLink;

[Alias("Starlight.NullLink.IServerGrain")]
public interface IServerGrain : IGrainWithStringKey
{
    [Public, Alias("UpdateServer")]
    public ValueTask UpdateServer(Server server);

    [Public, Alias("UpdateServerInfo")]
    public ValueTask UpdateServerInfo(ServerInfo info);


    [Public, Alias("GetPlayerData")]
    public ValueTask<PlayerData> GetPlayerData(Guid player);

    [Public, Alias("SetGhostTheme")]
    public ValueTask SetGhostTheme(Guid player, GhostTheme theme);

    [Public, Alias("UpdateResource")]
    public ValueTask UpdateResource(Guid player, string key, double value);
}

[GenerateSerializer]
[Alias("Starlight.NullLink.PlayerData")]
public sealed class PlayerData
{
    [Id(0)]
    public GhostTheme GhostTheme = new();
    [Id(1)]
    public Dictionary<string, double> Resources = [];
}

[GenerateSerializer]
[Alias("Starlight.NullLink.GhostTheme")]
public sealed record GhostTheme
{
    [Id(0)]
    public string Id { get; set; } = "None";

    [Id(1)]
    public Color GhostThemeColor = Color.White;
}
