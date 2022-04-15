using Timelog.AspNetCore.CommandRequests;
using Timelog.Core.ViewModels;

namespace Timelog.WebApp.ViewModels
{
    public class StatsViewModel
    {
        public FilterStatsRequest StatsRequest { get; set; }
        public TotalStatisticsVewModel TotalStats { get; set; }
    }
}
