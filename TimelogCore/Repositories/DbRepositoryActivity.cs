using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timelog.Entities;
using Timelog.Interfaces;

namespace Timelog.Repositories
{
    public class DbRepositoryActivity : DbRepositoryGeneric<UserActivityModel>, IRepositoryActivity
    {
        
        public DbRepositoryActivity(DbContext context) : base(context)
        {
        }

        public UserActivityModel getCurrentActivity()
        {
            //isUserConfigured();
            var projects = _context.Set<Project>();
            var activityTypes = _context.Set<ActivityType>();
            var row = items
                .Join(projects, a => a.ProjectId, p => p.Id, (activity, project) => new { activity, project })
                .Join(activityTypes, a => a.activity.ActivityTypeId, aType => aType.Id, (row, activityType) => new
                {
                    row.activity,
                    row.project,
                    activityType
                })
                .Where(x => x.activity.UserUniqId == UserGuid)
                .Where(x => x.activity.Status != UserActivityModel.ActivityStatus.Complite)
                .OrderByDescending(a => a.activity.StartTime)
                .FirstOrDefault();
            var currentActivity = row?.activity;
            if (currentActivity != null)
            {
                currentActivity.Project = row.project;
                currentActivity.ActivityType = row.activityType;
            }

            return currentActivity;
        }

        public override IEnumerable<UserActivityModel> GetAll()
        {
            //isUserConfigured();
            var projects = _context.Set<Project>();
            var activityTypes = _context.Set<ActivityType>();
            return items
            .Join(projects, a => a.ProjectId, p => p.Id, (activity, project) => new { activity, project })
            .Join(activityTypes, a => a.activity.ActivityTypeId, aType => aType.Id, (row, activityType) => new
            {
                row.activity,
                row.project,
                activityType
            })
            .Where(x => x.activity.UserUniqId == UserGuid)
            .Select(row => new UserActivityModel(row.activity, row.project, row.activityType));
        }


    }
}
