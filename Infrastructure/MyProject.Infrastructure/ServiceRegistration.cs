using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Application.Abstractions.Services;
using MyProject.Application.Repositories.Announcements;
using MyProject.Infrastructure.Services;

namespace MyProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void InjectInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var EmailConf = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(EmailConf);
            services.AddScoped<IEmailSender, EmailSender>();

        }
    }
}
