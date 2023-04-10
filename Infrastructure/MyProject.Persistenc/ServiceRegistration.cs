using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Application.Repositories.Announcements;
using MyProject.Persistenc.Repositres.Announcements;
using TurboAuto.Persistence.Contexts;

namespace MyProject.Persistenc
{
    public static class ServiceRegistration
    {
        public static void InjectPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("SqlServer") ?? throw new InvalidOperationException("Connection string  not found.");
            services.AddDbContext<TurboAutoDbContext>(db => db.UseSqlServer(connectionString));

            services.AddScoped<IAnnouncementReadRepository, AnnouncementReadRepository>();
            services.AddScoped<IAnnouncementWriteRepository, AnnouncementWriteRepository>();

        }
    }
}
