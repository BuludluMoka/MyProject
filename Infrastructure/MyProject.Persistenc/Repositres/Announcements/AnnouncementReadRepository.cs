using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyProject.Application.Repositories.Announcements;
using MyProject.Application.RequestParameters;
using MyProject.Domain.Entities;
using System.Data;
using TurboAuto.Persistence.Contexts;

namespace MyProject.Persistenc.Repositres.Announcements
{
    public class AnnouncementReadRepository : ReadRepository<Announcement>, IAnnouncementReadRepository
    {
        private readonly TurboAutoDbContext _context;

        public AnnouncementReadRepository(TurboAutoDbContext context) : base(context)
        {
            _context = context;
        }

        List<FilterAnnouncements> IAnnouncementReadRepository.GetAnnouncements(GetAnnounceFilterParams filters, out int totalRecords)
        {
            var startYearParam = new SqlParameter("@startYear", filters.StartYear ?? (object)DBNull.Value);
            var endYearParam = new SqlParameter("@endYear", filters.EndYear ?? (object)DBNull.Value);
            var startPriceParam = new SqlParameter("@startPrice", filters.StartPrice ?? (object)DBNull.Value);
            var endPriceParam = new SqlParameter("@endPrice", filters.EndPrice ?? (object)DBNull.Value);
            var currIdParam = new SqlParameter("@currId", filters.CurrId ?? (object)DBNull.Value);
            var banIdParam = new SqlParameter("@banId", filters.BanId ?? (object)DBNull.Value);
            var makeIdParam = new SqlParameter("@makeId", filters.MakeId ?? (object)DBNull.Value);
            var modelIdParam = new SqlParameter("@modelId", filters.ModelId ?? (object)DBNull.Value);
            var pageSizeParam = new SqlParameter("@pageSize", filters.PageSize);
            var pageIndexParam = new SqlParameter("@pageIndex", filters.PageIndex);
            var totalRecordsParam = new SqlParameter("@totalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };

            var result = _context.FilterAnnouncements.FromSqlRaw(
                "EXECUTE sp_AnnouncedCarsFilter @startYear, @endYear, @startPrice, @endPrice, @currId, @banId, @makeId, @modelId, @pageSize, @pageIndex, @totalRecords OUTPUT",
                startYearParam, endYearParam, startPriceParam, endPriceParam, currIdParam, banIdParam, makeIdParam, modelIdParam, pageSizeParam, pageIndexParam, totalRecordsParam)
                .ToList();

            totalRecords = (int)totalRecordsParam.Value;

            return result;
        }
    }
}
