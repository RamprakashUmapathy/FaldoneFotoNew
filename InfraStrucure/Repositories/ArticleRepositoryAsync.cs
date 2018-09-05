using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;

namespace InfraStrucure.Repositories
{

    public class ArticleRepositoryAsync : IArticleRepositoryAsync
    {
        public ArticleRepositoryAsync()
        {

        }

        public Task<Article> AddAsync(Article entity)
        {
            throw new NotImplementedException();
        }

        public void Create(Article item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Article item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Article entity)
        {
            throw new NotImplementedException();
        }

        public Task<Article> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Article>> ListAllAsync()
        {
            List<Article> list = new List<Article>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                var gridReader = await con.QueryAsync<Article>("dbo.ArticlesListAll_1",
                                                                commandType: CommandType.StoredProcedure);
                return gridReader;
            }
        }

        public Task<IEnumerable<Article>> ListAsync(dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Article entity)
        {
            throw new NotImplementedException();
        }
    }
}

