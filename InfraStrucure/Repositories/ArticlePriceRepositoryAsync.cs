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
    public class PriceListRepositoryAsync : IPriceListRepositoryAsync
    {
        public Task<PriceList> AddAsync(PriceList entity)
        {
            throw new NotImplementedException();
        }

        public void Create(PriceList item)
        {
            throw new NotImplementedException();
        }

        public void Delete(PriceList item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(PriceList entity)
        {
            throw new NotImplementedException();
        }

        public Task<PriceList> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PriceList>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PriceList>> ListAsync(dynamic parameters)
        {
            List<PriceList> list = new List<PriceList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                var gridReader = await con.QueryAsync<PriceList>("dbo.ArticleShopSignsList",
                                                                param: (object)parameters,
                                                                commandType: CommandType.StoredProcedure);
                return gridReader;
            }

        }

        public Task UpdateAsync(PriceList entity)
        {
            throw new NotImplementedException();
        }
    }
}

