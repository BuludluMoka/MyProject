using Microsoft.AspNetCore.Identity;

namespace MyProject.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public ICollection<Announcement> Announcements { get; set; }

    }
}
