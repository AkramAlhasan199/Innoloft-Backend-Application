using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Domain.Entities
{
    public abstract class Entity : Entity<string>
    {
        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    public abstract class Entity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public virtual DateTimeOffset CreatedOn { get; set; }

        public virtual DateTimeOffset UpdatedOn { get; set; }

    }

}
