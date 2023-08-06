using Innoloft_Backend_Application.DataTransferObjects.Users;
using Innoloft_Backend_Domain.Entities;

namespace Innoloft_Backend_Application.DataTransferObjects.Events
{
    public class EventDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatedById { get; set; }

        public virtual DateTimeOffset CreatedOn { get; set; }

        public virtual DateTimeOffset UpdatedOn { get; set; }

        public List<UserDto> Participants { get; set; }
    }
}
