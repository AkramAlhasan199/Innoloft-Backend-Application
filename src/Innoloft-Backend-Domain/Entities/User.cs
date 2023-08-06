using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Domain.Entities
{
    public class User : Entity
    {
        public User() 
        {
            Events = new List<Event>();
        }

        public string UserName { get; set; }

        // must be encrypted
        public string Password { get; set; }

        public List<Event> Events { get; set; }
    }
}
