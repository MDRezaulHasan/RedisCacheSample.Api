namespace RedisCacheSample.Api.Services;

public interface ICacheService
{
    T GetData<T>(string key);
    bool SetData<T>(string key, T value,DateTimeOffset expiretionTime);
    object RemoveData(string key);
}