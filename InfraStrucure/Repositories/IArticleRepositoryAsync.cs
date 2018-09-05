using InfraStrucure.Interfaces;
using ApplicationCore.Entity;

namespace InfraStrucure.Repositories
{
    public interface IArticleRepositoryAsync : IAsyncRepository<Article, string>
    {
    }

    public interface IArticlePriceRepositoryAsync : IAsyncRepository<ArticlePrice, string>
    {
    }
}
