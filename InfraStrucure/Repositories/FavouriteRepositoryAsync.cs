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
    public class FavouriteRepositoryAsync : IFavouriteRepositoryAsync
    {
        public async Task<Favourite> AddAsync(Favourite entity)
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Favourite entity)
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }

        public async Task<Favourite> GetByIdAsync(string userName, string name)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                var result = await con.QueryFirstAsync<Favourite>("dbo.FavouritesRead",
                                                                param: new { UserName = userName, Name = name },
                                                                commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<IEnumerable<Favourite>> ListAllAsync()
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Favourite>> ListAsync(dynamic parameters)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                var results = await con.QueryAsync<Favourite>("dbo.FavouritesList",
                                                                param: (object)parameters,
                                                                commandType: CommandType.StoredProcedure);
                return results;
            }
        }

        public async Task UpdateAsync(Favourite entity)
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }
    }
}
