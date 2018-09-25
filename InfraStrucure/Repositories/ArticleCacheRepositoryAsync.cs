using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Linq;

namespace InfraStrucure.Repositories
{
    public class ArticleCacheRepositoryAsync : ArticleRepositoryAsync
    {
        private IStockGroupRepositoryAsync _stockGroupRepository;
        private MemoryCache _cache;
        public ArticleCacheRepositoryAsync(IStockGroupRepositoryAsync stockGroupRepository) : base()
        {
            _stockGroupRepository = stockGroupRepository ?? throw new ArgumentNullException(nameof(stockGroupRepository));
            _cache = MemoryCache.Default;
        }
        public new async Task<IEnumerable<Article>> ListAllAsync()
        {
            string key = Guid.Parse("{8254CBB0-B172-42C8-9984-5D72EBABF039}").ToString();
            IEnumerable<Article> repositoryCache = null;
            if (_cache.GetCacheItem(key) != null)
                repositoryCache = _cache.GetCacheItem(key).Value as List<Article>;
            if (repositoryCache == null)
            {
                repositoryCache = await base.ListAllAsync();
                var stockGroups = await _stockGroupRepository.ListAllAsync();
                IDictionary<string, Article> articleDictionary = repositoryCache.ToDictionary(a => a.Id, b => b);
                foreach(StockGroup sg in stockGroups)
                {
                    if (articleDictionary.TryGetValue(sg.ArticleId, out Article art))
                    {
                        art.Add(sg);
                    }
                }
                DateTime dt = DateTime.Today.AddDays(1).AddHours(9); //9:00 AM ivalidate cache
                CacheItemPolicy policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = new DateTimeOffset(dt.Ticks, new TimeSpan(1, 0, 0))
                };
                _cache.Add(key.ToString(), repositoryCache, policy);
            }
            return repositoryCache;

        }

        public new async Task<IEnumerable<Article>> ListAsync(dynamic parameters)
        {
            await Task.Delay(1);
            throw new NotSupportedException("method not supported. use base class");
        }

        public async Task<IEnumerable<Article>> ListAsync(Func<Article, bool> predicate)
        {
            //Get all from cache and use predicate in memory data
            var cache = await ListAllAsync();
            return cache.Where(predicate);
        }


    }
}