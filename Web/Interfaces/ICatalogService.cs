using ApplicationCore.Entity;
using InfraStrucure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
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
        private readonly IPriceListRepositoryAsync _articlePriceRepository;
        private readonly IShopSignRepositoryAsync _shopSignRepository;
        private readonly IUserRepositoryAsync _userRepository;
        //private readonly IStockGroupRepositoryAsync _stockGroupRepository;

        public CatalogService(IArticleRepositoryAsync articleRepository, IPriceListRepositoryAsync articlePriceRepository,  IShopSignRepositoryAsync shopSignRepository, IUserRepositoryAsync userRepository)
        {
            _articleRepository = (ArticleCacheRepositoryAsync)articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _articlePriceRepository = articlePriceRepository ?? throw new ArgumentNullException(nameof(articlePriceRepository));
            _shopSignRepository = shopSignRepository ?? throw new ArgumentNullException(nameof(shopSignRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            //_stockGroupRepository = stockGroupRepository ?? throw new ArgumentNullException(nameof(stockGroupRepository));
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
            IEnumerable<Article> articles = new List<Article>();
            if (isPost)
            {
                IEnumerable<PriceList> prices = await _articlePriceRepository.ListAsync(new { model.ShopSignId, model.User.Rate });
                var ap = cache.Join(
                                    prices,
                                    a => a.Id,
                                    p => p.ArticleId,
                                    (a, p) => new
                                    {
                                        Article = a,
                                        PriceList = p
                                    })
                                    .Where(f => (f.Article.IsKasanova == (model.ShopSignId == "K" ? true : false)
                                                       || f.Article.IsKasanovaPiu == (model.ShopSignId == "K+" ? true : false)
                                                       || (f.Article.IsCoimport == (model.ShopSignId == "COI" ? true : false))
                                                       || (f.Article.IsOutlet == (model.ShopSignId == "ODK" ? true : false))
                                                       || (f.Article.IsItalianFactory == (model.ShopSignId == "IT" ? true : false))
                                                       || (f.Article.IsCasaSullAlbero == (model.ShopSignId == "CSA" ? true : false)))
                                                       && (string.IsNullOrEmpty(model.CategoryId) || f.Article.Category == model.CategoryId)
                                                       && (string.IsNullOrEmpty(model.FamilyId) || f.Article.Family == model.FamilyId)
                                                       && (string.IsNullOrEmpty(model.SeriesId) || f.Article.Series == model.SeriesId)
                                                       && (string.IsNullOrEmpty(model.Level1Id) || f.Article.Level1 == model.Level1Id)
                                                       && (string.IsNullOrEmpty(model.Level2Id) || f.Article.Level1 == model.Level2Id)
                                                       && (model.WebEnabledId == "10" || f.Article.IsMagentoEnabled == (model.WebEnabledId == "20" ? true : false))
                                                       && (model.VideoAvailableId == "10" || f.Article.HasVideo == (model.VideoAvailableId == "20" ? true : false))
                                                       && (model.MandatoryDeliveryId == "10" || f.PriceList.IsMandatoryPickup == (model.VideoAvailableId == "20" ? true : false))
                                                       //&& (model.WareHouseNameId == "10" || f.wa == (model.WareHouseNameId == "20" ? true : false))
                                                       && (model.DirectDeliveryId == "10" || f.PriceList.IsDirectDelivery == (model.DirectDeliveryId == "20" ? true : false))
                                                       && (model.ChalcoId == "10" || f.Article.HasChalcoData == (model.ChalcoId == "20" ? true : false))
                                                       && (model.PhotoId == "10" || f.Article.HasPhoto == (model.PhotoId == "20" ? true : false))
                                                       && (string.IsNullOrEmpty(model.StyleId) || model.StyleId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().Contains(f.Article.Style))
                                                       //&& (string.IsNullOrEmpty(model.StockGroupId) || f.StockGroups.Where(s => s.StockGroupId) )
                                                       )
                                      .ToList();

                ap.ForEach(f => model.Articles.Add(
                                    new ArticleLightViewModel()
                                    {
                                        Book = f.Article.Book,
                                        HasPhotoInChalco = f.Article.HasChalcoData,
                                        Description = f.Article.Description,
                                        Depth = f.Article.Depth,
                                        GrossRetailPrice = f.PriceList.GrossRetailPrice,
                                        Height = f.Article.Height,
                                        Id = f.Article.Id,
                                        IsDirectDelivery = f.PriceList.IsDirectDelivery,
                                        IsPrivateLabel = f.PriceList.IsPrivateLabel,
                                        IsMandatoryPickup = f.PriceList.IsMandatoryPickup,
                                        Line = f.Article.Line,
                                        Materials = f.Article.Materials,
                                        NameAlias = f.Article.NameAlias,
                                        NetRetailPrice = f.PriceList.NetRetailPrice,
                                        GrossSalesPrice = f.PriceList.GrossSalesPrice,
                                        RetailDiscountPercentage = f.PriceList.RetailDiscountPercentage,
                                        Weight = f.Article.Weight,
                                        Width = f.Article.Width,
                                        Youtube = f.Article.Youtube,
                                        Tag = f.PriceList.Tag,
                                        StockQuantity = f.Article.StockQuantity,
                                        VendorStockQuantity = f.Article.VendorStockQuantity
                                    }));

                articles = ap.Select(f => f.Article).ToList();

            }
            else
            {

                IEnumerable<PriceList> prices = await _articlePriceRepository.ListAsync(new { model.ShopSignId, model.User.Rate });
                articles = cache.Join(
                                    prices,
                                    a => a.Id,
                                    p => p.ArticleId,
                                    (a, p) => a);
                articles = cache;

            }

            //Categoria
            var categories = cache.Select(f => f.Category).Distinct()
                                        .OrderBy(f => f); //TODO add condition for filter shop sign
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

            PrepareListItems(model, articles);

            //Style filter (DevExpress List)
            var styles = articles.Select(f => f.Style)
                                    .OrderBy(f => f)
                                    .Distinct()
                                    .ToList();
            model.StyleItems = styles;
            //SetSelectedValue(model.StyleItems, model.StyleId);

            //StockGroups filter (DevExpress List)
            var stockGroups = articles.SelectMany(f => f.StockGroups)
                                        .OrderBy(f => f.StockGroupId)
                                        .Select(f => f.StockGroupId)
                                        .Distinct();
            model.StockGroupItems = stockGroups.ToList();
            //SetSelectedValue(model.StockGroupItems, model.StockGroupId);

            //SupplyStatus filter (DevExpress List)
            IEnumerable<string> supplyStatus = articles.SelectMany(f => f.StockGroups)
                                                        .OrderBy(f => f.LastSupplyStatusId)
                                                        .Select(s => s.LastSupplyStatusId)
                                                        .Distinct();

            model.SupplyStatusItems = supplyStatus.ToList();
            //SetSelectedValue(model.SupplyStatusItems, model.SupplyStatusId);

            //Tag commerciale filter (DevExpress List)
            List<string> tags = model.Articles.Select(f => f.Tag)
                                                    .OrderBy(f => f)
                                                    .Distinct()
                                                    .ToList();
            model.TagItems = tags;

            return model;
        }

        private void PrepareListItems(HomeViewModel model, IEnumerable<Article> articles)
        {

            //Famiglia
            IEnumerable<string> families = null;
            if (string.IsNullOrEmpty(model.CategoryId))
            {
                families = articles.Select(f => f.Family)
                                     .OrderBy(f => f)
                                     .Distinct();

            }
            else
            {
                families = articles.Where(f => f.Category == model.CategoryId)
                                    .Select(f => f.Family)
                                    .OrderBy(f => f)
                                    .Distinct();

            }
            var items = new List<SelectListItem>()
            {
                new SelectListItem() { Value = string.Empty, Text = "--", Selected = true }
            };
            foreach (string family in families)
            {
                items.Add(new SelectListItem() { Text = family, Value = family });
            }
            model.Families = items;
            SetSelectedValue(model.Families, model.FamilyId);

            //Serie
            IEnumerable<string> series = new List<string>();
            if (string.IsNullOrEmpty(model.CategoryId) && string.IsNullOrEmpty(model.FamilyId))
            {
                series = articles.Select(f => f.Series)
                                     .OrderBy(f => f)
                                     .Distinct();

            }
            else
            {
                series = articles.Where(f => f.Category == model.CategoryId && f.Family == model.FamilyId)
                                    .Select(f => f.Series)
                                     .OrderBy(f => f)
                                     .Distinct();

            }

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

            //Livello1
            var level1s = articles.Select(f => f.Level1).Distinct()
                                    .OrderBy(f => f);
            items = new List<SelectListItem>()
            {
                new SelectListItem() { Value = string.Empty, Text = "--", Selected = true }
            };
            foreach (string level1 in level1s)
            {
                items.Add(new SelectListItem() { Text = level1, Value = level1 });
            }
            model.Level1s = items;
            SetSelectedValue(model.Level1s, model.Level1Id);

            //Livello2
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

            //Disponiblità web filter
            model.WebEnabledItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "Abilitati" },
                new SelectListItem() { Value = "30", Text = "Disabilitati" },
            };
            SetSelectedValue(model.WebEnabledItems, model.WebEnabledId);

            //Youtube filter
            model.VideoAvailableItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "Solo Youtube video" },
                new SelectListItem() { Value = "30", Text = "Senza Youtube video" },
            };
            SetSelectedValue(model.VideoAvailableItems, model.VideoAvailableId);

            //Magazzino filter
            model.WareHouseItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "Kasanova" },
                new SelectListItem() { Value = "30", Text = "Mastercasa" },
                new SelectListItem() { Value = "40", Text = "Baimex" }
            };
            SetSelectedValue(model.WareHouseItems, model.WareHouseNameId);

            //Chalco filter
            model.ChalcoItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "In Chalco" },
                new SelectListItem() { Value = "30", Text = "Non in Chalco" },
            };
            SetSelectedValue(model.ChalcoItems, model.ChalcoId);

            //Foto filter
            model.PhotoItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "Solo con foto" },
                new SelectListItem() { Value = "30", Text = "Senza foto" },
            };
            SetSelectedValue(model.PhotoItems, model.PhotoId);

            //Colore filter
            model.ColorItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "COL" },
                new SelectListItem() { Value = "30", Text = "NOCOL" },
            };
            SetSelectedValue(model.ColorItems, model.ColorId);

            //Ritiro Obbligatorio Filter
            model.MandatoryDeliveryItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "Ritiro obbligatorio" },
                new SelectListItem() { Value = "30", Text = "No ritiro obbligatorio" },
            };
            SetSelectedValue(model.MandatoryDeliveryItems, model.MandatoryDeliveryId);

            //Consegna diretta Items
            model.DirectDeliveryItems = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "10", Text = "Tutti", Selected = true },
                new SelectListItem() { Value = "20", Text = "Solo consegna diretta" },
                new SelectListItem() { Value = "30", Text = "No consegna diretta" },
            };
            SetSelectedValue(model.DirectDeliveryItems, model.DirectDeliveryId);

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
