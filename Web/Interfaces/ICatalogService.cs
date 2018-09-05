using ApplicationCore.Entity;
using InfraStrucure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Extensions;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface ICatalogService
    {
        Task<HomeViewModel> GetCatalogItems();

        Task<HomeViewModel> GetCatalogItems(HomeViewModel model, bool isPost);
    }

    public class CatalogService : ICatalogService
    {
        private readonly ArticleCacheRepositoryAsync _articleRepository;
        private readonly IArticlePriceRepositoryAsync _articlePriceRepository;
        private readonly IShopSignRepositoryAsync _shopSignRepository;
        private readonly IUserRepositoryAsync _userRepository;

        public CatalogService(IArticleRepositoryAsync articleRepository, IArticlePriceRepositoryAsync articlePriceRepository, IShopSignRepositoryAsync shopSignRepository, IUserRepositoryAsync userRepository)
        {
            _articleRepository = (ArticleCacheRepositoryAsync) articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _articlePriceRepository = articlePriceRepository ?? throw new ArgumentNullException(nameof(articlePriceRepository));
            _shopSignRepository = shopSignRepository ?? throw new ArgumentNullException(nameof(shopSignRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<HomeViewModel> GetCatalogItems()
        {
            HomeViewModel model = new HomeViewModel();
            return await GetCatalogItems(model, false);
        }

        public async Task<HomeViewModel> GetCatalogItems(HomeViewModel model, bool isPost)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            //Set user
            model.User = await _userRepository.GetByIdAsync(Thread.CurrentPrincipal.Identity.Name);

            var shops = await _shopSignRepository.ListAllAsync();

            List<SelectListItem> shopItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = string.Empty, Text = "--", Selected = true }
            };
            foreach (ShopSign shop in shops)
            {
                shopItems.Add(new SelectListItem() { Text = shop.Name, Value = shop.Id });
            }
            model.ShopSigns = shopItems;

            var cache = await _articleRepository.ListAllAsync();
            IEnumerable<Article> articles = null;
            if (isPost)
            {
                var prices = await _articlePriceRepository.ListAsync(new { model.ShopSignId, model.User.Rate });
                articles = cache.Where(f => (f.IsKasanova == (model.ShopSignId == "K" ? true : false) || f.IsKasanovaPiu == (model.ShopSignId == "K+" ? true : false)
                                                       || (f.IsCoimport == (model.ShopSignId == "COI" ? true : false)) || (f.IsOutlet == (model.ShopSignId == "ODK" ? true : false))
                                                            || (f.IsItalianFactory == (model.ShopSignId == "IT" ? true : false)) || (f.IsCasaSullAlbero == (model.ShopSignId == "CSA" ? true : false)))
                                                       && (string.IsNullOrEmpty(model.CategoryId) || f.Category == model.CategoryId)
                                                       && (string.IsNullOrEmpty(model.FamilyId) || f.Family == model.FamilyId)
                                                       && (string.IsNullOrEmpty(model.SeriesId) || f.Series == model.SeriesId)
                                                       && (string.IsNullOrEmpty(model.Level1Id) || f.Level1 == model.Level1Id)
                                                       && (string.IsNullOrEmpty(model.Level2Id) || f.Level1 == model.Level2Id)
                                                       && (model.PhotoId == "10" || f.HasPhoto == (model.PhotoId == "20" ? true : false))
                                                       && (model.ChalcoId == "10" || f.HasChalcoData == (model.ChalcoId == "20" ? true : false)));

                model.Articles = articles.ToArticlesViewModel(prices);
            }
            else
            { 
                articles = cache;
            }

            //Category
            var categories = cache.Select(f => f.Category).Distinct()
                                        .OrderBy(f => f);
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem() { Value = string.Empty, Text = "--", Selected = true }
            };
            foreach (string category in categories)
            {
                items.Add(new SelectListItem() { Text = category, Value = category });
            }
            model.Categories = items;
            SetSelectedValue(items, model.CategoryId);

            //Family
            var families = articles.Select(f => f.Family).Distinct()
                                        .OrderBy(f => f);
            items = new List<SelectListItem>()
            {
                new SelectListItem() { Value = string.Empty, Text = "--", Selected = true }
            };
            foreach(string family in families)
            {
                items.Add(new SelectListItem() { Text = family, Value = family });
            }
            model.Families = items;
            SetSelectedValue(model.Families, model.FamilyId);

            //Series
            var series = articles.Select(f => f.Series).Distinct()
                                        .OrderBy(f => f);
            items = new List<SelectListItem>()
            {
                new SelectListItem() { Value = string.Empty, Text = "--", Selected = true }
            };
            foreach (string serie in series)
            {
                items.Add(new SelectListItem() { Text = serie, Value = serie });
            }
            model.Series = items;
            SetSelectedValue(model.Series, model.SeriesId);

            //Level1
            var level1s = articles.Select(f => f.Level1).Distinct()
                                    .OrderBy(f => f);
            items = new List<SelectListItem>()
            {
                new SelectListItem() { Value = string.Empty, Text = "--", Selected = true }
            };
            foreach(string level1 in level1s)
            {
                items.Add(new SelectListItem() { Text = level1, Value = level1 });
            }
            model.Level1s = items;
            SetSelectedValue(model.Level1s, model.Level1Id);

            //Level2
            var level2s = articles.Select(f => f.Level2).Distinct()
                                    .OrderBy(f => f);
            items = new List<SelectListItem>()
            {
                new SelectListItem() { Value = string.Empty, Text = "--", Selected = true }
            };
            foreach (string level2 in level2s)
            {
                items.Add(new SelectListItem() { Text = level2, Value = level2 });
            }
            model.Level2s = items;
            SetSelectedValue(model.Level2s, model.Level2Id);

            model.WebEnabledItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "Abilitati" },
                new SelectListItem() { Value = "30", Text = "Disabilitati" },
            };
            SetSelectedValue(model.WebEnabledItems, model.WebEnabledId);


            model.VideoAvailableItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "Solo Youtube video" },
                new SelectListItem() { Value = "30", Text = "Senza Youtube video" },
            };
            SetSelectedValue(model.VideoAvailableItems, model.VideoAvailableId);

            model.WareHouseItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "Solo Youtube video" },
                new SelectListItem() { Value = "30", Text = "Senza Youtube video" },
            };
            SetSelectedValue(model.WareHouseItems, model.WareHouseNameId);

            model.ChalcoItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "In Chalco" },
                new SelectListItem() { Value = "30", Text = "Non in Chalco" },
            };
            SetSelectedValue(model.ChalcoItems, model.ChalcoId);

            model.PhotoItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "Solo con foto" },
                new SelectListItem() { Value = "30", Text = "Senza foto" },
            };
            SetSelectedValue(model.PhotoItems, model.PhotoId);

            model.ColorItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "COL" },
                new SelectListItem() { Value = "30", Text = "NOCOL" },
            };
            SetSelectedValue(model.ColorItems, model.ColorId);

            var styles = articles.Select(f => f.Style).Distinct()
                                    .OrderBy(f => f) ;
            List<SelectListItem> styleItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = string.Empty, Text = "--", Selected = true }
            };
            foreach (string style in styles)
            {
                styleItems.Add(new SelectListItem() { Text = style, Value = style });
            }
            model.StyleItems = styleItems;
            SetSelectedValue(model.StyleItems, model.StyleId);

            return model;
        }

        private void SetSelectedValue(IEnumerable<SelectListItem> items, string value)
        {
            var web = items.SingleOrDefault(f => f.Value == value);
            if (web != null)
            {
                web.Selected = true;
            }
        }
    }
}
