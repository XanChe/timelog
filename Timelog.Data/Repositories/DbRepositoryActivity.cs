using Microsoft.EntityFrameworkCore;
using Timelog.Core.Entities;
using Timelog.Core.Repositories;

namespace Timelog.Data.Repositories
{
    public class DbRepositoryActivity : DbRepositoryGeneric<UserActivity>, IRepositoryActivity
    {
        public DbRepositoryActivity(DbContext context) : base(context)
        {
        }
        public async Task<UserActivity?> getCurrentActivityAsync()
        {
            //isUserConfigured();
            var projects = _context.Set<Project>();
            var activityTypes = _context.Set<ActivityType>();
            var row = await items
                .Join(projects, a => a.ProjectId, p => p.Id, (activity, project) => new { activity, project })
                .Join(activityTypes, a => a.activity.ActivityTypeId, aType => aType.Id, (row, activityType) => new
                {
                    row.activity,
                    row.project,
                    activityType
                })
                .Where(x => x.activity.UserUniqId == UserGuid)
                .Where(x => x.activity.Status != ActivityStatus.Complite)
                .OrderByDescending(a => a.activity.StartTime)
                .FirstOrDefaultAsync();
            var currentActivity = row?.activity;
            if (currentActivity != null)
            {
                currentActivity.Project = row?.project;
                currentActivity.ActivityType = row?.activityType;
            }

            return currentActivity;
        }
        public override async Task<IEnumerable<UserActivity>> GetAllAsync()
        {
            //isUserConfigured();
            var projects = _context.Set<Project>();
            var activityTypes = _context.Set<ActivityType>();

            var activities =
                from a in items
                join p in projects on a.ProjectId equals p.Id
                join t in activityTypes on a.ActivityTypeId equals t.Id 
                where a.UserUniqId == UserGuid
                select a;

            return await activities.ToListAsync();
        }

        public Task StartActivityAsync(Guid projectId, Guid activityTypeId)
        {
            throw new NotImplementedException();
        }

        public Task StopActivityAsync(string comment)
        {
            throw new NotImplementedException();
        }
    }
}
