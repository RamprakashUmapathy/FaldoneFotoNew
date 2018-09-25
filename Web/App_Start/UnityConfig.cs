using InfraStrucure.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Web.Interfaces;

namespace Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IArticleRepositoryAsync, ArticleCacheRepositoryAsync>();
            container.RegisterType<IPriceListRepositoryAsync, PriceListRepositoryAsync>();
            container.RegisterType<IStockGroupRepositoryAsync, StockGroupRepositoryAsync>();
            container.RegisterType<IShopSignRepositoryAsync, ShopSignRepositoryAsync>();
            container.RegisterType<IUserRepositoryAsync, UserRepositoryAsync>();
            container.RegisterType<ICatalogService, CatalogService>();
            container.RegisterType<IFavouriteRepositoryAsync, FavouriteRepositoryAsync>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}