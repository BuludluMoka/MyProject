using MyProject.Application.Repositories.Announcements;
using MyProject.Domain.Entities;
using TurboAuto.Persistence.Contexts;

namespace MyProject.Persistenc.Repositres.Announcements
{
    public class AnnouncementWriteRepository : WriteRepository<Announcement>, IAnnouncementWriteRepository
    {
        public AnnouncementWriteRepository(TurboAutoDbContext context) : base(context)
        {

        }
    }
}
