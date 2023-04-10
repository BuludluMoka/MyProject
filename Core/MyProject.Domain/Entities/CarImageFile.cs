using MyProject.Domain.Entities.Common;

namespace MyProject.Domain.Entities
{
    public class CarImageFile : BaseEntity
    {
        public string Image { get; set; }
        public long AnnouncementId { get; set; }
        public Announcement Announcement { get; set; }

    }
}
