using ApplicationCore.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InfraStrucure.Repositories
{
    public class ArticlePriceRepositoryAsync : IArticlePriceRepositoryAsync
    {
        public Task<ArticlePrice> AddAsync(ArticlePrice entity)
        {
            throw new NotImplementedException();
        }

        public void Create(ArticlePrice item)
        {
            throw new NotImplementedException();
        }

        public void Delete(ArticlePrice item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ArticlePrice entity)
        {
            throw new NotImplementedException();
        }

        public Task<ArticlePrice> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ArticlePrice>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ArticlePrice>> ListAsync(dynamic parameters)
        {
            List<ArticlePrice> list = new List<ArticlePrice>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                var gridReader = await con.QueryAsync<ArticlePrice>("dbo.ArticleShopSignsList",
                                                                param: (object)parameters,
                                                                commandType: CommandType.StoredProcedure);
                return gridReader;
            }

        }

        public Task UpdateAsync(ArticlePrice entity)
        {
            throw new NotImplementedException();
        }
    }
}

