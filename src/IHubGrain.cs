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
public interface IHubGrain : IObservableDictionaryCrudGrain<Server>, IObservableDictionaryCrudGrain<ServerInfo>, IGrainWithIntegerKey
{
    [Public, Alias("GetAndSubscribe<Server>")]
    public new ValueTask<Dictionary<string, Server>> GetAndSubscribe(IObserverDictionaryCrudGrain<Server> observer);
    [Public, Alias("Resubscribe<Server>")]
    public new ValueTask Resubscribe(IObserverDictionaryCrudGrain<Server> observer);
    [Public, Alias("Unsubscribe<Server>")]
    public new ValueTask Unsubscribe(IObserverDictionaryCrudGrain<Server> observer);

    [Public, Alias("GetAndSubscribe<ServerInfo>")]
    public new ValueTask<Dictionary<string, ServerInfo>> GetAndSubscribe(IObserverDictionaryCrudGrain<ServerInfo> observer);
    [Public, Alias("Resubscribe<ServerInfo>")]
    public new ValueTask Resubscribe(IObserverDictionaryCrudGrain<ServerInfo> observer);
    [Public, Alias("Unsubscribe<ServerInfo>")]
    public new ValueTask Unsubscribe(IObserverDictionaryCrudGrain<ServerInfo> observer);
}

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
