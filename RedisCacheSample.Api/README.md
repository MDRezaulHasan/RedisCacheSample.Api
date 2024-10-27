## RedisCacheSample.Api
#### Added Nuget packages
1. `dotnet add RedisCacheSample.Api package Microsoft.EntityFrameworkCore.Sqlite`
2. `dotnet add RedisCacheSample.Api package Microsoft.EntityFrameworkCore`
3. `dotnet add RedisCacheSample.Api package Microsoft.EntityFrameworkCore.Design`
4. `dotnet add RedisCacheSample.Api package Microsoft.Extensions.Caching.StackExchangeRedis`
5. `dotnet add RedisCacheSample.Api package StackExchange.Redis`
#### DB Migrration
`dotnet ef migrations add "initial-migration" --project RedisCacheSample.Api`
`dotnet ef database update --project RedisCacheSample.Api`
 
#### This application needs a redis official docker image for running redis server.
[Redis Docker Image](https://hub.docker.com/_/redis/tags)
### How to run redis image 
`docker run --name redis-cache-local -p 6379:6379 redis`