using DevExpress.Web.Mvc;
using InfraStrucure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ICatalogService _catalogService;
        private readonly string modelKey = $"{Thread.CurrentPrincipal.Identity.Name}-HomeViewModel";

        public HomeController(ICatalogService catalogService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        }

        // GET: Home
        [HttpGet()]
        public async Task<ActionResult> Index()
        {
            HomeViewModel model = await _catalogService.GetCatalogItems();
            return View(model);
        }

        // POST: Home
        [HttpPost()]
        public async Task<ActionResult> Index(HomeViewModel model)
        {
            var m = await _catalogService.GetCatalogItems(model, true);
            //Work around required for DevExpress partial action method
            StoreModelToCache(model.Articles);
            return View(m);
        }

        [ValidateInput(false)]
        public ActionResult CardViewPartial()
        {
            var selectedArticles = Request.Params["selectedArticles"];
            if (!string.IsNullOrEmpty(selectedArticles))
            {
                //User selected articles

            }
            var articles = GetArticlesFromCache();
            return PartialView("_CardViewPartial", articles);
        }

        private void StoreModelToCache(IEnumerable<ArticleLightViewModel> articles)
        {
            var cache = MemoryCache.Default;
            cache.Remove(modelKey);
            DateTime dt = DateTime.Now.AddMinutes(20);
            CacheItemPolicy policy = new CacheItemPolicy
            {
                SlidingExpiration = TimeSpan.FromMinutes(20)
            };
            cache.Add(modelKey, articles, policy);
        }

        private IEnumerable<ArticleLightViewModel> GetArticlesFromCache()
        {
            var cacheItem = MemoryCache.Default.GetCacheItem(modelKey) ?? throw new NullReferenceException("Cache key not found"); ;
            return cacheItem.Value as IEnumerable<ArticleLightViewModel>;
        }

        //public async Task<PartialViewResult> RoundedPanelContent()
        //{
        //    HomeViewModel model = await _catalogService.GetCatalogItems();
        //    return PartialView(model);
        //}

    }
}