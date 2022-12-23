using System;
namespace EmployeeManagement.Core.Caching
{
	public interface ICacheProvider<T>
	{
		Task<IEnumerable<T>> GetCachedResponse(string cacheKey);
		Task<IEnumerable<T>> GetCachedResponse(string cacheKey, SemaphoreSlim semaphoreSlim);
    }
}

