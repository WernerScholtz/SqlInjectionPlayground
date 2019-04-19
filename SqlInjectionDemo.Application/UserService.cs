using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace SqlInjectionDemo.Application
{
    public class UserService : IUserService
    {
        private readonly IConfiguration config;

        public UserService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<List<User>> GetUnsafe(string searchName)
        {
            using (var conn = Connection)
            {
                var query = $"SELECT * FROM [dbo].[Users] WHERE Name LIKE ('%{searchName}%')";
                var result = await conn.QueryAsync<User>(query);
                return result.ToList();
            }
        }

        public async Task<List<User>> GetSafe(string searchName)
        {
            using (var conn = Connection)
            {
                var query = "SELECT * FROM [dbo].[Users] WHERE Name LIKE @SearchName";

                var result = await conn.QueryAsync<User>(query, new
                {
                    SearchName = "%" + searchName + "%"
                });

                return result.ToList();
            }
        }

        private IDbConnection Connection
        {
            get
            {
                var connection = new SqlConnection(config.GetConnectionString("MyConnectionString"));
                connection.Open();
                return connection;
            }
        }
    }
}
