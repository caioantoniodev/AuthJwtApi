using AuthJwtApi.Application.Ports.Out;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AuthJwtApi.Infra.Repositories;

public class CacheRepository : ICacheRepositoryPortOut
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public CacheRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public async Task<string> GetCacheValueAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        return (await db.StringGetAsync(key))!;    }

    public async Task SetCacheValueAsync(string key, object value)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(key, JsonConvert.SerializeObject(value));
    }
}