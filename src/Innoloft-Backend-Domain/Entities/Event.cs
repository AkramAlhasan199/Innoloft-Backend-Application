using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Domain.Entities
{
    public class Event : Entity
    {
        public Event() 
        {
            Participants = new List<User>();
        }
        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatedById { get; set; }

        public User CreatedBy { get; set; }

        public List<User> Participants { get; set; }

    }
}
