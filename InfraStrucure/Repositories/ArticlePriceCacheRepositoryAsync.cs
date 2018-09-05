using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace InfraStrucure.Repositories
{
    [Obsolete("Donot use", true)]
    public class ArticlePriceCacheRepositoryAsync : ArticlePriceRepositoryAsync
    {
        private MemoryCache _cache;

        public ArticlePriceCacheRepositoryAsync()
        {
            _cache = MemoryCache.Default;
        }

        public new async Task<IEnumerable<ArticlePrice>> ListAsync(dynamic parameters)
        {
            IEnumerable<ArticlePrice> repositoryCache = null;
            string shopSignId = parameters.GetType().GetProperty("shopsignid").GetValue(parameters, null);
            string key = string.Concat("ArticlePriceList_", shopSignId);

            if (_cache.GetCacheItem(key) != null)
                repositoryCache = _cache.GetCacheItem(key).Value as List<ArticlePrice>;
            if (repositoryCache == null)
            {
                repositoryCache = await base.ListAsync((object)parameters);
                DateTime dt = DateTime.Today.AddDays(1).AddHours(8); //8:00 AM ivalidate cache
                CacheItemPolicy policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = new DateTimeOffset(dt.Ticks, new TimeSpan(1, 0, 0))
                };
                _cache.Add(key.ToString(), repositoryCache, policy);
            }
            return repositoryCache;
        }
    }

}
