using Timelog.Core.ViewModels;
using Timelog.Core.Entities;
using Timelog.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Timelog.Data.Repositories
{
    public class DbRepositiryStatistics : IRepositirySatistics
    {
        private readonly TimelogDbContext _context;
        private Guid userGuid;
       

        public DbRepositiryStatistics(TimelogDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ActivityTypeStatisticsViewModel>> GetActivityTypeStatsForPeriodAsync(ActivityType activityType, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProjectStatisticsViewModel>> GetProjectStatsForPeriodAsync(Project project, DateTime fromDate, DateTime toDate)
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

            var listReult = new List<ProjectStatisticsViewModel>();

            foreach (var projectActivities in items)
            {
                listReult.Add(new ProjectStatisticsViewModel()
                {
                    ProjectName = projectActivities.ProjectName,
                    FirstActivity = projectActivities.FirstActivity,
                    LastActivity = projectActivities.LastActivity,
                    ActivityCount = projectActivities.ActivityCount,
                    DurationInSecondsTotal = projectActivities.DurationInSecondsTotal,
                    DurationInSecondsAvarage = projectActivities.DurationInSecondsAvarage,
                    DurationInSecondsMin = projectActivities.DurationInSecondsMin,
                    DurationInSecondsMax = projectActivities.DurationInSecondsMax

                });               
            }
            return listReult;
        }

        public async Task<TotalStatisticsVewModel> GetTotalStatisticsForPeriodAsync(DateTime from, DateTime to)
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
                FirstActivity = (await filtredActivities.FirstAsync()).StartTime,
                LastActivity = (await filtredActivities.LastAsync()).EndTime,
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
