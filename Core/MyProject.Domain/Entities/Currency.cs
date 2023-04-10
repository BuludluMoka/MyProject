using MyProject.Domain.Entities.Common;

namespace MyProject.Domain.Entities
{
    public class Currency
    {
        public byte Id { get; set; }
        public string CurrencyCode { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
    }
}
