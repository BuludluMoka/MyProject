using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SP_AnnouncementFilterList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
             CREATE OR ALTER PROC sp_AnnouncedCarsFilter
               @startYear INT = NULL,
               @endYear INT = NULL,
               @startPrice DECIMAL(18, 2) = NULL,
               @endPrice DECIMAL(18, 2) = NULL,
               @currId INT = NULL,
               @banId INT = NULL,
               @makeId INT = NULL,
               @modelId INT = NULL,
               @pageSize INT = 8,
               @pageIndex INT = 1,
               @totalRecords INT OUTPUT
             AS
             BEGIN
               SET NOCOUNT ON;
             
               SELECT @totalRecords = COUNT(*) FROM Announcements;
             
               SELECT a.Id, 
                      make.MakeName, 
                      model.ModelName, 
                      a.Price, 
                      a.Year, 
                      banType.Ban, 
                      c.CurrencyCode AS Currency, 
                      img.Image
               FROM Announcements AS a
               JOIN Models AS model ON a.ModelId = model.Id
               JOIN Makes AS make ON model.MakeId = make.Id
               JOIN BanTypes AS banType ON a.BanTypeId = banType.Id
               JOIN Currencies AS c ON a.CurrencyId = c.Id
               OUTER APPLY (
                   SELECT TOP 1 Image FROM CarImageFiles AS f WHERE f.AnnouncementId = a.Id
               ) AS img
               WHERE (@startYear IS NULL OR a.Year >= @startYear) AND (@endYear IS NULL OR a.Year <= @endYear)
                     AND (@startPrice IS NULL OR a.Price >= @startPrice) AND (@endPrice IS NULL OR a.Price <= @endPrice)
                     AND (@currId IS NULL OR c.Id = @currId) AND (@banId IS NULL OR banType.Id = @banId)
                     AND (@makeId IS NULL OR make.Id = @makeId) AND (@modelId IS NULL OR model.Id = @modelId)
                 ORDER BY a.Id OFFSET (@pageIndex - 1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY;
             END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP PROC sp_AnnouncedCarsFilter");
        }
    }
}
