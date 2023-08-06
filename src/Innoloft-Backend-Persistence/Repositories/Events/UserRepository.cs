using Innoloft_Backend_Domain.Entities;
using Innoloft_Backend_Domain.Repositories;
using Innoloft_Backend_Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innoloft_Backend_Domain.Repositories.Users;

namespace Innoloft_Backend_Persistence.Repositories.Events
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
        }
    }
}
