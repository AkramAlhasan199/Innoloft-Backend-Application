using Innoloft_Backend_Domain.Entities;
using Innoloft_Backend_Domain.Repositories.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Innoloft_Backend_Domain.Services.Events
{
    public interface IEventService : ICrudService<Event>
    {
    }

    public class EventService : CrudService<Event>, IEventService
    {
        public EventService(IEventRepository repository) : base(repository)
        {
        }
    }
}
