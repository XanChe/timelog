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
        public async Task<IEnumerable<ActivityTypeStatViewModel>> GetActivityTypeStatsForPeriodAsync(DateTime fromDate, DateTime toDate)
        {
            var items = await (
                from a in _context.UserActivities
                join t in _context.ActivityTypes on a.ActivityTypeId equals t.Id
                where a.UserUniqId == userGuid && fromDate <= a.StartTime && a.StartTime <= toDate
                group a by new { a.ActivityTypeId, t.Name } into agroup
                select new
                {
                    ActivityTypeName = agroup.Key.Name,
                    ActivityTypeId = agroup.Key.ActivityTypeId,
                    FirstActivity = agroup.Min(a => a.StartTime),
                    LastActivity = agroup.Max(a => a.EndTime),
                    ActivityCount = agroup.Count(),
                    DurationInSecondsTotal = (long)agroup.Sum(a => (a.EndTime - a.StartTime).TotalSeconds),
                    DurationInSecondsAvarage = (long)agroup.Average(a => (a.EndTime - a.StartTime).TotalSeconds),
                    DurationInSecondsMin = (long)agroup.Min(a => (a.EndTime - a.StartTime).TotalSeconds),
                    DurationInSecondsMax = (long)agroup.Max(a => (a.EndTime - a.StartTime).TotalSeconds)
                }).ToListAsync();

            var listReult = new List<ActivityTypeStatViewModel>();

            foreach (var activityTypeActivities in items)
            {
                listReult.Add(new ActivityTypeStatViewModel()
                {
                    ActivityTypeName = activityTypeActivities.ActivityTypeName,
                    FirstActivity = activityTypeActivities.FirstActivity,
                    LastActivity = activityTypeActivities.LastActivity,
                    ActivityCount = activityTypeActivities.ActivityCount,
                    DurationInSecondsTotal = activityTypeActivities.DurationInSecondsTotal,
                    DurationInSecondsAvarage = activityTypeActivities.DurationInSecondsAvarage,
                    DurationInSecondsMin = activityTypeActivities.DurationInSecondsMin,
                    DurationInSecondsMax = activityTypeActivities.DurationInSecondsMax

                });
            }
            return listReult;
        }

        public async Task<IEnumerable<ProjectStatViewModel>> GetProjectStatsForPeriodAsync(DateTime fromDate, DateTime toDate)
        {
            
            var items = await (
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
                }).ToListAsync();

            var listReult = new List<ProjectStatViewModel>();

            foreach (var projectActivities in items)
            {
                listReult.Add(new ProjectStatViewModel()
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
            var filtredActivities = await _context
                .UserActivities
                .Where(activity => activity.UserUniqId == userGuid)
                .Where(activity => activity.StartTime > from && activity.EndTime < to)
                .OrderBy(activity => activity.StartTime)
                .Select(activity => new
                {
                    StartTime = activity.StartTime,
                    EndTime = activity.EndTime,
                    Duration = (activity.EndTime - activity.StartTime).TotalSeconds
                }).ToListAsync();

            return new TotalStatisticsVewModel()
            {
                FirstActivity = ( filtredActivities.First()).StartTime,
                LastActivity = ( filtredActivities.Last()).EndTime,
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
