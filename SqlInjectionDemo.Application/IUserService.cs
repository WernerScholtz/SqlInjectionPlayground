using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlInjectionDemo.Application
{
    public interface IUserService
    {
        Task<List<User>> GetUnsafe(string searchName);
        Task<List<User>> GetSafe(string searchName);
    }
}
