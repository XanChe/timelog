using Microsoft.AspNetCore.Mvc;
using Timelog.AspNetCore.Services;
using Timelog.Core;
using Timelog.Core.Services;

namespace Timelog.WebApp.Controllers
{
    public class StatsController : Controller
    {
        private ITimelogServiceBuilder _timelogServiceBuilder;
        private IStatisticsService _statisticsService;

        public StatsController(TimelogAspService timelogAspService)
        {
            _timelogServiceBuilder = timelogAspService.TimelogServiceBuilder;
            _statisticsService = _timelogServiceBuilder.CreateStatisticsService();
        }
        public IActionResult Index()
        {
            return View(_statisticsService.GetTotalStatistics( new DateTime(2022, 3,1), DateTime.Now));
        }

        public IActionResult ByProject()
        {
            return View(_statisticsService.GetProjectStatistics(new DateTime(2022, 3, 1), DateTime.Now));
        }
    }
}
