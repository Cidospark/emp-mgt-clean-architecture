using System;
using System.Linq;
using EmployeeManagement.Core.Caching;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Enums;
using EmployeeManagement.Core.Repositories;
using EmployeeManagement.Core.Repositories.Base;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EmployeeManagement.Infrastructure.Caching
{
	public class CacheProvider<T>: ICacheProvider<T> where T : class
    {
        private readonly IMemoryCache memoryCache;
        private readonly EmployeeContext _context;
        private static readonly SemaphoreSlim GetDataSemaphore = new SemaphoreSlim(1, 1);

        public CacheProvider(IMemoryCache memoryCache, EmployeeContext context)
		{
            this.memoryCache = memoryCache;
            _context = context;
        }

        public async Task<IEnumerable<T>> GetCachedResponse(string cacheKey)
        {
            try
            {
                return await GetCachedResponse(cacheKey, GetDataSemaphore);
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
                data = await _context.Set<T>().ToListAsync();

                var memoryCacheEntryOpts = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024
                };
                memoryCache.Set(cacheKey, data, memoryCacheEntryOpts);
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

