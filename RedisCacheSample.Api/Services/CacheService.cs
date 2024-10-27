using System.Text.Json;
using StackExchange.Redis;

namespace RedisCacheSample.Api.Services;

public class CacheService:ICacheService
{
    IDatabase _database;

    public CacheService()
    {
        //Redis connection 
        _database = ConnectionMultiplexer.Connect("localhost:6379").GetDatabase();
    }
    public T GetData<T>(string key)
    {
        var value = _database.StringGet(key);
        if (value.HasValue)
            return JsonSerializer.Deserialize<T>(value);
        return default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expiretionTime)
    {
        var expiry = expiretionTime.DateTime.Subtract(DateTime.Now);
        return _database.StringSet(key, JsonSerializer.Serialize(value), expiry);
    }

    public object RemoveData(string key)
    {
        var existValue = _database.KeyExists(key);
        if (!existValue)
            return false;
        return _database.KeyDelete(key);
    }
}