using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timelog.Interfaces;
using Timelog.Core.ViewModels;
using Timelog.Entities;

namespace Timelog.Services
{
    public class StatisticsService
    {
        private readonly IRepositirySatistics _repositirySatistics;
        public StatisticsService(IRepositirySatistics repositirySatistics)
        {
            this._repositirySatistics = repositirySatistics;
        }

        public TotalStatisticsVewModel GetTotalStatistics(DateTime from, DateTime to)
        {
            return _repositirySatistics.GetTotalStatisticsForPeriod(from, to);
        }

        public IEnumerable<ProjectStatisticsViewModel> GetProjectStatistics( DateTime from, DateTime to)
        {
            return _repositirySatistics.GetProjectStatsForPeriod(new Project(), from, to);
        }

    }
}
