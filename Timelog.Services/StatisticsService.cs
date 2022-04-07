using Timelog.Core.Entities;
using Timelog.Core.Repositories;
using Timelog.Core.ViewModels;
using Timelog.Core.Services;
using Timelog.Core;

namespace Timelog.Services
{
    public class StatisticsService: IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatisticsService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<TotalStatisticsVewModel> GetTotalStatistics(DateTime from, DateTime to)
        {
            return await _unitOfWork.Satistics.GetTotalStatisticsForPeriodAsync(from, to);
        }

        public async Task<IEnumerable<ProjectStatisticsViewModel>> GetProjectStatistics( DateTime from, DateTime to)
        {
            return await _unitOfWork.Satistics.GetProjectStatsForPeriodAsync(new Project(), from, to);
        }

    }
}
