namespace MyProject.Domain.Entities
{
    public class BanType
    {
        public byte Id { get; set; }
        public string Ban { get; set; }
        public ICollection<Announcement> Announcements { get; set; }

    }
}
