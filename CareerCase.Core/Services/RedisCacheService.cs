using CareerCase.Core.Services.Interfaces;
using ServiceStack.Redis;
using System;

namespace CareerCase.Core.Services
{
    public class RedisCacheService : ICacheService
    {
        public bool Contains(string cacheKey)
        {
            using (var client = new RedisClient())
            {
                return client.ContainsKey(cacheKey);
            }
        }

        public T Get<T>(string cacheKey)
        {
            using (var client = new RedisClient())
            {
                var redisdata = client.Get<T>(cacheKey);
                return redisdata;
            }
        }

        public void Remove(string cacheKey)
        {
            using (IRedisClient client = new RedisClient())
            {
                client.Remove(cacheKey);
            }
        }

        public void Set<T>(string cacheKey, T model, TimeSpan timeSpan)
        {
            using (IRedisClient client = new RedisClient())
            {
                if (client.SearchKeys(cacheKey).Count == 0)
                {
                    var cachedata = client.As<T>();
                    cachedata.SetValue(cacheKey, model, timeSpan);
                }
            }
        }
    }
}