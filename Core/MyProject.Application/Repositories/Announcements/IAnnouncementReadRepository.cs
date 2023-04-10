using MyProject.Application.RequestParameters;
using MyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.Repositories.Announcements
{
    public interface IAnnouncementReadRepository : IReadRepository<Announcement>
    {
        List<FilterAnnouncements> GetAnnouncements(GetAnnounceFilterParams filters, out int totalRecords);
    }
}
