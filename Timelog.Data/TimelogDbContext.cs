using Microsoft.EntityFrameworkCore;
using Timelog.Core.Entities;

namespace Timelog.Data
{
    public class TimelogDbContext: DbContext
    {
#nullable enable
        public DbSet<Project> Projects { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public virtual DbSet<UserActivity> UserActivities { get; set; }
#nullable disable
        public TimelogDbContext(DbContextOptions<TimelogDbContext> options)
           : base(options)
        {
            
        }       
    }
}
