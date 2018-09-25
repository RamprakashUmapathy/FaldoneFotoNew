using ApplicationCore.Entity;
using InfraStrucure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class FavouritesController : Controller
    {

        private readonly IFavouriteRepositoryAsync _favouriteRepository;
        private readonly ArticleCacheRepositoryAsync _articleRepository;

        public FavouritesController(IFavouriteRepositoryAsync favouriteRepository, IArticleRepositoryAsync articleRepository)
        {
            _favouriteRepository = favouriteRepository ?? throw new ArgumentNullException(nameof(favouriteRepository));
            var ar = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            ArticleCacheRepositoryAsync repository = ar as ArticleCacheRepositoryAsync ;
            _articleRepository = repository ?? throw new ArgumentException($"The type of {nameof(articleRepository)} should be ArticleCacheRepositoryAsync");
        }

        // GET: Favourite
        public async Task<ActionResult> Index()
        {
            IEnumerable<Favourite> favourites = await _favouriteRepository.ListAsync(new { UserName = Thread.CurrentPrincipal.Identity.Name });
            List<FavouriteViewModel> model = new List<FavouriteViewModel>();

            var cache = await _articleRepository.ListAllAsync();
            //IEnumerable<PriceList> prices = await _articlePriceRepository.ListAsync(new { model.ShopSignId, model.User.Rate });

            foreach (Favourite fav in favourites)
            {
                model.Add(new FavouriteViewModel()
                {
                    Name = fav.Name,
                    User = fav.UserName,
                    LastUpdate = fav.LastUpdate
                });
            }
            return View(model);
        }
    }
}