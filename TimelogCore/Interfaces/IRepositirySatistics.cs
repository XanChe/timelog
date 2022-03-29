using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timelog.Core.ViewModels;
using Timelog.Entities;

namespace Timelog.Interfaces
{
    public interface IRepositirySatistics
    {
        public void SetUser(Guid userIdentityGuid);
        public TotalStatisticsVewModel GetTotalStatisticsForPeriod(DateTime from, DateTime to);
        public IEnumerable<ProjectStatisticsViewModel> GetProjectStatsForPeriod(Project project, DateTime from, DateTime to);
        public IEnumerable<ActivityTypeStatisticsViewModel> GetActivityTypeStatsForPeriod(ActivityType activityType, DateTime from, DateTime to);
    }
}
