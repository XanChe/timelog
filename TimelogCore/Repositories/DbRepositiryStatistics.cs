
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

        public IEnumerable<ProjectStatisticsViewModel> GetProjectStatsForPeriod(Project project, DateTime fromDate, DateTime toDate)
        {
            
            var items =
                from a in _context.UserActivities
                join p in _context.Projects on a.ProjectId equals p.Id
                where a.UserUniqId == userGuid && fromDate <= a.StartTime && a.StartTime <= toDate
                group a by new { a.ProjectId, p.Name } into agroup
                select new
                {                    
                    ProjectName = agroup.Key.Name,
                    ProjectId = agroup.Key.ProjectId,
                    FirstActivity = agroup.Min(a => a.StartTime),
                    LastActivity = agroup.Max(a => a.EndTime),
                    ActivityCount = agroup.Count(),
                    DurationInSecondsTotal = (long)agroup.Sum(a => (a.EndTime - a.StartTime).TotalSeconds),
                    DurationInSecondsAvarage = (long)agroup.Average(a => (a.EndTime - a.StartTime).TotalSeconds),
                    DurationInSecondsMin = (long)agroup.Min(a => (a.EndTime - a.StartTime).TotalSeconds),
                    DurationInSecondsMax = (long)agroup.Max(a => (a.EndTime - a.StartTime).TotalSeconds)
                };

            foreach (var projectActivities in items)
            {
                var result = new ProjectStatisticsViewModel()
                {
                    ProjectName = projectActivities.ProjectName,
                    FirstActivity = projectActivities.FirstActivity,
                    LastActivity = projectActivities.LastActivity,
                    ActivityCount = projectActivities.ActivityCount,
                    DurationInSecondsTotal = projectActivities.DurationInSecondsTotal,
                    DurationInSecondsAvarage = projectActivities.DurationInSecondsAvarage,
                    DurationInSecondsMin = projectActivities.DurationInSecondsMin,
                    DurationInSecondsMax = projectActivities.DurationInSecondsMax

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
