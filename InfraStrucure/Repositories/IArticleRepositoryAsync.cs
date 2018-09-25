using InfraStrucure.Interfaces;
using ApplicationCore.Entity;

namespace InfraStrucure.Repositories
{
    public interface IArticleRepositoryAsync : IAsyncRepository<Article, string>
    {
    }

    public interface IPriceListRepositoryAsync : IAsyncRepository<PriceList, string>
    {
    }

    public interface IStockGroupRepositoryAsync : IAsyncRepository<StockGroup, string>
    {
    }
}
