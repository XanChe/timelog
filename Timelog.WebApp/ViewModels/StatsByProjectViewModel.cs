using Timelog.AspNetCore.CommandRequests;
using Timelog.Core.ViewModels;

namespace Timelog.WebApp.ViewModels
{
    public class StatsByProjectViewModel
    {
        public FilterStatsRequest StatsRequest { get; set; }
        public IEnumerable<ProjectStatViewModel> ProjectItems { get; set; }
    }
}
