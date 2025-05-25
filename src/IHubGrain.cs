using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Starlight.NullLink.Abstract;
using Starlight.NullLink.Attributes;

namespace Starlight.NullLink;

[Alias("Starlight.NullLink.IHubGrain")]
public interface IHubGrain : IGrainWithIntegerKey
{
    [Public, Alias("GetAndSubscribe<Server>")]
    public  ValueTask<Dictionary<string, Server>> GetAndSubscribe(IServerObserver observer);
    [Public, Alias("Resubscribe<Server>")]
    public  ValueTask Resubscribe(IServerObserver observer);
    [Public, Alias("Unsubscribe<Server>")]
    public  ValueTask Unsubscribe(IServerObserver observer);

    [Public, Alias("GetAndSubscribe<ServerInfo>")]
    public  ValueTask<Dictionary<string, ServerInfo>> GetAndSubscribe(IServerInfoObserver observer);
    [Public, Alias("Resubscribe<ServerInfo>")]
    public  ValueTask Resubscribe(IServerInfoObserver observer);
    [Public, Alias("Unsubscribe<ServerInfo>")]
    public  ValueTask Unsubscribe(IServerInfoObserver observer);
}

[Alias("Starlight.NullLink.IServerObserver")]
public interface IServerObserver : IObserverDictionaryCrudGrain<Server>, IGrainObserver;

[Alias("Starlight.NullLink.IServerInfoObserver")]
public interface IServerInfoObserver : IObserverDictionaryCrudGrain<ServerInfo>, IGrainObserver;

[GenerateSerializer]
[Alias("Starlight.NullLink.Server")]
public record Server
{
    [Id(0)]
    public required string Title { get; set; }
    [Id(1)]
    public string? Description { get; set; }
    [Id(2)]
    public ServerType Type { get; set; } = ServerType.NRP;
    [Id(3)]
    public bool IsAdultOnly { get; set; } = false;
    [Id(4)]
    public required string ConnectionString { get; set; }
}
[GenerateSerializer]
[Alias("Starlight.NullLink.ServerInfo")]
public record ServerInfo
{
    [Id(0)]
    public DateTime? СurrentStateStartedAt { get; set; }
    [Id(1)]
    public int? MaxPlayers { get; set; }
    [Id(2)]
    public int? Players { get; set; }
    [Id(3)]
    public ServerStatus Status { get; set; } = ServerStatus.Offline;
}
[GenerateSerializer]
public enum ServerStatus : byte
{
    Offline,
    Lobby,
    Round,
    RoundEnding,
}
[GenerateSerializer]
public enum ServerType : byte
{                                                           
    NRP,
    LRP_minus,
    LRP,
    LRP_plus,
    MRP_minus,
    MRP,
    MRP_plus,
    HRP_minus,
    HRP,
    HRP_plus,
}
