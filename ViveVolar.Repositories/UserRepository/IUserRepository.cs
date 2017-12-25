using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveVolar.Entities;
using ViveVolar.Repositories.Base;

namespace ViveVolar.Repositories.UserRepository
{
    public interface IUserRepository: IRepository<UserEntity>
    {
    }
}
