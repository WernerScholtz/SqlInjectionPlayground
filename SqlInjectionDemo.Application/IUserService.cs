using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlInjectionDemo.Application
{
    public interface IUserService
    {
        Task<List<User>> Get();
        Task<List<User>> GetById(int id);

    }
}
