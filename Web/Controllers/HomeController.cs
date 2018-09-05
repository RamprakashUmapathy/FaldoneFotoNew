using DevExpress.Web.Mvc;
using InfraStrucure.Repositories;
using System;
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
        private readonly IArticleRepositoryAsync _articleRepository;


        public HomeController(ICatalogService catalogService, IArticleRepositoryAsync articleRepository)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
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
            return View(m);
        }

        [ValidateInput(false)]
        public ActionResult CardViewPartial()
        {
            var model = CardViewExtension.GetViewModel("cardView");
            return PartialView("_CardViewPartial", model);
        }

    }
}