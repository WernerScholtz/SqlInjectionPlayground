using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SqlInjectionDemo.Application
{
    public class UserService : IUserService
    {
        public Task<List<User>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
