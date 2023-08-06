using Innoloft_Backend_Domain.Entities;
using Innoloft_Backend_Domain.Repositories;
using Innoloft_Backend_Domain.Repositories.Events;
using Innoloft_Backend_Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Persistence.Repositories.Events
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
        }
    }
}
