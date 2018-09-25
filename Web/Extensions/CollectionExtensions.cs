using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.ViewModels;

namespace Web.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<ArticleLightViewModel> ToArticlesViewModel(this IEnumerable<Article> coll, IEnumerable<PriceList> articlePrices)
        {
            var results = new List<ArticleLightViewModel>();
            var articles = coll ?? throw new ArgumentNullException(nameof(coll));

            var list = articles.Join(
                                articlePrices,
                                a => a.Id,
                                p => p.ArticleId,
                                (a, p) => new
                                {
                                    a.Book,
                                    a.Category,
                                    a.Id,
                                    a.Description,
                                    a.StockQuantity,
                                    a.VendorStockQuantity,
                                    a.ItemBarCode,
                                    a.Family,
                                    a.HasChalcoData,
                                    a.Materials,
                                    a.Line,
                                    a.Level1,
                                    a.Level2,
                                    a.MasterQuantity,
                                    a.NameAlias,
                                    a.Depth,
                                    a.Height,
                                    a.Width,
                                    a.Weight,
                                    a.Series,
                                    a.IsMagentoEnabled,
                                    a.Style,
                                    a.Youtube,
                                    a.StockGroups,
                                    p.IsDirectDelivery,
                                    p.IsMandatoryPickup,
                                    p.IsPrivateLabel,
                                    p.GrossRetailPrice,
                                    p.GrossSalesPrice,
                                    p.NetRetailPrice,
                                    p.RetailDiscountPercentage,
                                    p.Tag
                                }
                            ).ToList();

            list.ForEach(a =>
            {
                results.Add(new ArticleLightViewModel()
                {
                    Book = a.Book,
                    HasPhotoInChalco = a.HasChalcoData,
                    Description = a.Description,
                    Depth = a.Depth,
                    GrossRetailPrice = a.GrossRetailPrice,
                    Height = a.Height,
                    Id = a.Id,
                    IsDirectDelivery = a.IsDirectDelivery,
                    IsPrivateLabel = a.IsPrivateLabel,
                    IsMandatoryPickup = a.IsMandatoryPickup,
                    Line = a.Line,
                    Materials = a.Materials,
                    NameAlias = a.NameAlias,
                    NetRetailPrice = a.NetRetailPrice,
                    GrossSalesPrice = a.GrossSalesPrice,
                    RetailDiscountPercentage = a.RetailDiscountPercentage,
                    Weight = a.Weight,
                    Width = a.Width,
                    Youtube = a.Youtube,
                    Tag = a.Tag
                });
            });
            return results;
        }
    }
}