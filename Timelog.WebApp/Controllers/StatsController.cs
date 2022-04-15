using Microsoft.AspNetCore.Mvc;
using Timelog.AspNetCore.CommandRequests;
using Timelog.AspNetCore.Services;
using Timelog.Core;
using Timelog.Core.Services;
using Timelog.WebApp.ViewModels;

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
        public async Task<IActionResult> Index()
        {
            var nowDate = DateTime.Now;

            var filterRequest = new FilterStatsRequest()
            {
                FromDate = new DateTime(nowDate.Year, nowDate.Month, 1),
                ToDate = nowDate
            };


            var viewModel = new StatsViewModel()
            {
                StatsRequest = filterRequest,
                TotalStats = await _statisticsService.GetTotalStatisticsAsync(filterRequest.FromDate, filterRequest.ToDate)
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(FilterStatsRequest StatsRequest)
        {
            var viewModel = new StatsViewModel()
            {
                StatsRequest = StatsRequest,
                TotalStats = await _statisticsService.GetTotalStatisticsAsync(StatsRequest.FromDate, StatsRequest.ToDate)
            };

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ByProject()
        {
            var nowDate = DateTime.Now;

            var filterRequest = new FilterStatsRequest()
            {
                FromDate = new DateTime(nowDate.Year, nowDate.Month, 1),
                ToDate = nowDate
            };

            
            var viewModel = new StatsByProjectViewModel()
            {
                StatsRequest = filterRequest,
                ProjectItems = await _statisticsService.GetProjectStatisticsAsync(filterRequest.FromDate, filterRequest.ToDate)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ByProject(FilterStatsRequest StatsRequest)
        {
            var viewModel = new StatsByProjectViewModel()
            {
                StatsRequest = StatsRequest,
                ProjectItems = await _statisticsService.GetProjectStatisticsAsync(StatsRequest.FromDate, StatsRequest.ToDate)
            };
            return View(viewModel);
        }

    }
}
