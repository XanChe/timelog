using Timelog.Core.ViewModels;

namespace Timelog.Core.Services
{
    public interface IStatisticsService
    {
        public Task<TotalStatisticsVewModel> GetTotalStatistics(DateTime from, DateTime to);
        public Task<IEnumerable<ProjectStatisticsViewModel>> GetProjectStatistics(DateTime from, DateTime to);
    }
}
