using Innoloft_Backend_Domain.Entities;
using Innoloft_Backend_Domain.Repositories.Events;
using Innoloft_Backend_Domain.Repositories.Users;
using Innoloft_Backend_Domain.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Domain.Services.Users
{
    public interface IUserService : ICrudService<User>
    {
    }

    public class UserService : CrudService<User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }
    }
}
