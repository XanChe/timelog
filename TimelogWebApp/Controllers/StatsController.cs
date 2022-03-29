using Microsoft.AspNetCore.Mvc;
using Timelog.Services;

namespace Timelog.WebApp.Controllers
{
    public class StatsController : Controller
    {
        private TimelogServiceBuilder _timelogServiceBuilder;
        private StatisticsService _statisticsService;

        public StatsController(TimelogAspService timelogAspService)
        {
            _timelogServiceBuilder = timelogAspService.TimelogService;
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
