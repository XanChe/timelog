using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timelog.Core.ViewModels;
using Timelog.Core.Entities;

namespace Timelog.Core.Repositories
{
    public interface IRepositirySatistics
    {
        public void SetUser(Guid userIdentityGuid);
        public Task<TotalStatisticsVewModel> GetTotalStatisticsForPeriodAsync(DateTime from, DateTime to);
        public Task<IEnumerable<ProjectStatisticsViewModel>> GetProjectStatsForPeriodAsync(Project project, DateTime from, DateTime to);
        public Task<IEnumerable<ActivityTypeStatisticsViewModel>> GetActivityTypeStatsForPeriodAsync(ActivityType activityType, DateTime from, DateTime to);
    }
}
