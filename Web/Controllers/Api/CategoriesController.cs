using ApplicationCore.Entity;
using InfraStrucure.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Web.Controllers.Api
{
    public class CategoriesController : ApiController
    {

        private readonly ArticleCacheRepositoryAsync _articleRepository;
        private readonly IPriceListRepositoryAsync _articlePriceRepository;
        private readonly IShopSignRepositoryAsync _shopSignRepository;
        private readonly IUserRepositoryAsync _userRepository;

        public CategoriesController(IArticleRepositoryAsync articleRepository, IPriceListRepositoryAsync articlePriceRepository, IShopSignRepositoryAsync shopSignRepository, IUserRepositoryAsync userRepository)
        {
            _articleRepository = (ArticleCacheRepositoryAsync)articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _articlePriceRepository = articlePriceRepository ?? throw new ArgumentNullException(nameof(articlePriceRepository));
            _shopSignRepository = shopSignRepository ?? throw new ArgumentNullException(nameof(shopSignRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync(string shopSignId)
        {

            var categories = new List<string>();
            var articles = await _articleRepository.ListAllAsync();
            var user = await _userRepository.GetByIdAsync(Thread.CurrentPrincipal.Identity.Name);
            IEnumerable<PriceList> prices = await _articlePriceRepository.ListAsync(new { shopSignId, user.Rate });
            var results = articles.Join(
                                prices,
                                a => a.Id,
                                p => p.ArticleId,
                                (a, p) => a.Category);
            return results.Distinct();

        }
    }
}