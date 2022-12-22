using System;
using EmployeeManagement.Core.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace EmployeeManagement.Infrastructure.Caching
{
	public class CacheProvider<T>: ICacheProvider<T>
	{
        private readonly IMemoryCache memoryCache;
        private static readonly SemaphoreSlim GetDataSemaphore = new SemaphoreSlim(1, 1); 

        public CacheProvider(IMemoryCache memoryCache)
		{
            this.memoryCache = memoryCache;
        }

        public async Task<IEnumerable<T>> GetCachedResponse()
        {
            try
            {
                return await GetCachedResponse(CacheKeys.Users.ToString(), GetDataSemaphore);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetCachedResponse(string cacheKey, SemaphoreSlim semaphoreSlim)
        {
            bool isAvailable = memoryCache.TryGetValue(cacheKey, out List<T> data);
            if (isAvailable) return data;
            try
            {
                await semaphoreSlim.WaitAsync();
                isAvailable = memoryCache.TryGetValue(cacheKey, out data);

                // if data doesn't already exists in cache set it
                var result = await unitOfWork.Users.All();
                users = result.ToList();
                var memoryCacheEntryOpts = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024
                };
                memoryCache.Set(cacheKey, users, memoryCacheEntryOpts);
            }
            catch
            {
                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }
            return data;
        }
    }
}

