using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Starlight.NullLink.Abstract;

namespace Starlight.NullLink;
public interface IHubGrain : IObservableDictionaryCrudGrain<Server>, IObservableDictionaryCrudGrain<ServerInfo>, IGrainWithIntegerKey
{ }

[GenerateSerializer]
[Alias("Starlight.NullLink.Server")]
public record Server
{
    [Id(0)]
    public required string Title { get; set; }
    [Id(1)]
    public string? Description { get; set; }
    [Id(2)]
    public string? Image { get; set; }
    [Id(3)]
    public ServerType Type { get; set; } = ServerType.NRP;
    [Id(4)]
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
