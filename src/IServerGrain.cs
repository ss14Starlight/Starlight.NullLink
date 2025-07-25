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

    [Public, Alias("GetRoles")]
    public ValueTask<ulong[]> GetPlayerRoles(Guid player);
}