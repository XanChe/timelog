
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timelog.Core.ViewModels;
using Timelog.EF;
using Timelog.Entities;
using Timelog.Interfaces;

namespace Timelog.Repositories
{
    public class DbRepositiryStatistics : IRepositirySatistics
    {
        private readonly TimelogDbContext _context;
        private Guid userGuid;
       

        public DbRepositiryStatistics(TimelogDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ActivityTypeStatisticsViewModel> GetActivityTypeStatsForPeriod(ActivityType activityType, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectStatisticsViewModel> GetProjectStatsForPeriod(Project project, DateTime from, DateTime to)
        {
            var filtredActivities = _context
               .UserActivities
               .Where(activity => activity.UserUniqId == userGuid)
               .Where(activity => activity.StartTime > from && activity.EndTime < to)
               .OrderBy(activity => activity.StartTime)
               .Join(_context.Projects,
                    a => a.ProjectId,
                    p => p.Id,
                    (activity, project) => new
                    {
                        Project = project,
                        StartTime = activity.StartTime,
                        EndTime = activity.EndTime,
                        Duration = (activity.EndTime - activity.StartTime).TotalSeconds
                    }
               ).AsEnumerable()
               .GroupBy(row => row.Project.Id);

            foreach (var projectActivities in filtredActivities)
            {
                var result = new ProjectStatisticsViewModel()
                {
                    Project = projectActivities.FirstOrDefault().Project,
                    FirstActivity = projectActivities.First().StartTime,
                    LastActivity = projectActivities.Last().EndTime,
                    ActivityCount = projectActivities.Count(),
                    DurationInSecondsTotal = (long)projectActivities.Sum(a => a.Duration),
                    DurationInSecondsAvarage = (long)projectActivities.Average(a => a.Duration),
                    DurationInSecondsMin = (long)projectActivities.Min(a => a.Duration),
                    DurationInSecondsMax = (long)projectActivities.Max(a => a.Duration)

                };
                yield return result;
            }
        }

        public TotalStatisticsVewModel GetTotalStatisticsForPeriod(DateTime from, DateTime to)
        {
            var filtredActivities = _context
                .UserActivities
                .Where(activity => activity.UserUniqId == userGuid)
                .Where(activity => activity.StartTime > from && activity.EndTime < to)
                .OrderBy(activity => activity.StartTime)
                .Select(activity => new
                {
                    StartTime = activity.StartTime,
                    EndTime = activity.EndTime,
                    Duration = (activity.EndTime - activity.StartTime).TotalSeconds
                });
            return new TotalStatisticsVewModel()
            {
                FirstActivity = filtredActivities.First().StartTime,
                LastActivity = filtredActivities.Last().EndTime,
                ActivityCount = filtredActivities.Count(),
                DurationInSecondsTotal = (long)filtredActivities.Sum(a => a.Duration),
                DurationInSecondsAvarage = (long)filtredActivities.Average(a => a.Duration),
                DurationInSecondsMin = (long)filtredActivities.Min(a => a.Duration),
                DurationInSecondsMax = (long)filtredActivities.Max(a => a.Duration)
            };
        }

        public void SetUser(Guid userIdentityGuid)
        {
            userGuid =  userIdentityGuid;
        }
    }
}
