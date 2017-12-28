using System.Threading.Tasks;
using ViveVolar.Entities;
using ViveVolar.Models;
using ViveVolar.Services.Base;

namespace ViveVolar.Services.UserService
{
    public interface IUserService: IBaseService<User>
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Create(User user, string password);
    }
}
