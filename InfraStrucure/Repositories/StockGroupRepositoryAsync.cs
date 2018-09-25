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

    public class StockGroupRepositoryAsync : IStockGroupRepositoryAsync
    {
        public StockGroupRepositoryAsync()
        {

        }

        public Task<StockGroup> AddAsync(StockGroup entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(StockGroup entity)
        {
            throw new NotImplementedException();
        }

        public Task<StockGroup> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StockGroup>> ListAllAsync()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                var gridReader = await con.QueryAsync<StockGroup>("dbo.ArticleStockGroupsListAll",
                                                                commandType: CommandType.StoredProcedure);
                return gridReader;
            }
        }

        public Task<IEnumerable<StockGroup>> ListAsync(dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(StockGroup entity)
        {
            throw new NotImplementedException();
        }
    }
}

