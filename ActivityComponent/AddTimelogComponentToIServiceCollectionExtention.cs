using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Timelog.EF;
using Timelog.Interfaces;

namespace Timelog.Services
{
    public static class AddTimelogComponentToIServiceCollectionExtention
    {
        public static IServiceCollection AddTimelogComponent(this IServiceCollection services, string connectionString)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services");
            }
            services.AddDbContext<TimelogDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IRepositoryManager, DbRepositoryManager>();
            services.AddScoped<TimelogServiceBuilder>();           
            return services;               
        }
    }
}
