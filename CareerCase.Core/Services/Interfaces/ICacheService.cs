using System;

namespace CareerCase.Core.Services.Interfaces
{
    public interface ICacheService
    {
        void Set<T>(string cacheKey, T model, TimeSpan timeSpan);
        T Get<T>(string cacheKey);
        bool Contains(string cacheKey);
        void Remove(string cacheKey);
    }
}