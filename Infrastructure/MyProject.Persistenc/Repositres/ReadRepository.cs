using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyProject.Application.Repositories;
using MyProject.Application.RequestParameters;
using MyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TurboAuto.Persistence.Contexts;

namespace MyProject.Persistenc.Repositres
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly TurboAutoDbContext _context;
        public ReadRepository(TurboAutoDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();
        public List<FilterAnnouncements> GetAnnouncements(GetAnnounceFilterParams filters, out int totalRecords)
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

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }
    }
}
