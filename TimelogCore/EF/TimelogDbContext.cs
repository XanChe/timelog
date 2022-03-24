using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Timelog.Entities;

namespace Timelog.EF
{
    public class TimelogDbContext: DbContext
    {  
        public DbSet<Project> Projects { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public virtual DbSet<UserActivityModel> UserActivities { get; set; }
        public TimelogDbContext(DbContextOptions<TimelogDbContext> options)
           : base(options)
        {
            
        }       
    }
}
