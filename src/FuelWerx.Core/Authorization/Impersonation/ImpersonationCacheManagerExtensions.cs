using Abp.Runtime.Caching;
using System;
using System.Runtime.CompilerServices;

namespace FuelWerx.Authorization.Impersonation
{
	public static class ImpersonationCacheManagerExtensions
	{
		public static ITypedCache<string, ImpersonationCacheItem> GetImpersonationCache(this ICacheManager cacheManager)
		{
			return cacheManager.GetCache<string, ImpersonationCacheItem>("AppImpersonationCache");
		}
	}
}