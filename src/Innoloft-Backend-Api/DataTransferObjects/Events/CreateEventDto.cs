namespace Innoloft_Backend_Application.DataTransferObjects.Events
{
    public class CreateEventDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual DateTimeOffset CreatedOn { get; set; }

        public virtual DateTimeOffset UpdatedOn { get; set; }
    }
}
