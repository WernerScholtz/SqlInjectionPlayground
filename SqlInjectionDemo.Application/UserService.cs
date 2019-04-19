using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using SqlInjectionDemo.Common;

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
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                foreach (var unsafeKeyword in UnsafeKeywords)
                {
                    if (searchName.ToLower().Contains(unsafeKeyword))
                    {
                        throw new FunSpoilerException("Although dropping or deleting something would probably have worked, let's not spoil the fun too soon. Rather try something else for now please. :)");
                    }
                }
            }

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

        private List<string> UnsafeKeywords =>
            new List<string>
            {
                "drop",
                "delete"
            };
    }
}
