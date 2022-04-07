using Microsoft.Extensions.DependencyInjection;
using Timelog.AspNetCore.Services;
using Timelog.Core;
using Timelog.Data;
using Timelog.Services;

namespace Timelog.AspNetCore.Extensions
{
    public static class TivelogServicesExtention
    {   
        public static IServiceCollection AddTimelogServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services");
            }
            services.AddScoped<IUnitOfWork, DbUnitOfWork>();
            services.AddScoped<ITimelogServiceBuilder, TimelogServiceBuilder>();
            services.AddTransient<TimelogAspService>();          

            return services;
        }
    }
}
