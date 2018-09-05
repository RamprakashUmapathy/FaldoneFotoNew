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
    public class UserRepositoryAsync : IUserRepositoryAsync
    {
        public Task<User> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                var user = await con.QuerySingleAsync<User>("dbo.UserSettingsRead",
                                                                param: new { username = id},
                                                                commandType: CommandType.StoredProcedure);
                return user;
            }

        }

        public Task<IEnumerable<User>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> ListAsync(dynamic parameters)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
