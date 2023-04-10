using Microsoft.Extensions.DependencyInjection;
using MyProject.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application
{
    public static class ServiceRegistration
    {
        public static void InjectApplicationService(this IServiceCollection services)
        {
            services.AddScoped<ImageManager>();
        }
    }
}
