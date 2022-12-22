using System;
namespace EmployeeManagement.Infrastructure.Caching
{
	public interface ICacheProvider<T>
	{
		Task<IEnumerable<T>> GetCachedResponse();
		Task<IEnumerable<T>> GetCachedResponse(string cacheKey, SemaphoreSlim semaphoreSlim);
    }
}

