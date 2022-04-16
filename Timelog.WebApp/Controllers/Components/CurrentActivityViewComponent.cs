using Microsoft.AspNetCore.Mvc;
using Timelog.AspNetCore.Services;
using Timelog.Core;
using Timelog.Core.Services;
using System.Linq;

namespace Timelog.WebApp.Controllers.Components
{
    public class CurrentActivity:ViewComponent
    {
        private const double BAR_CHANE_RATIO = 1.75;
        private ITimelogServiceBuilder _timelogServiceBuilder;
        private IUserActivityService _activityManager;

        public CurrentActivity(TimelogAspService timelogAspService)
        {
            _timelogServiceBuilder = timelogAspService.TimelogServiceBuilder;
            _activityManager = _timelogServiceBuilder.CreateUserActivityService();
        }        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentActivity = await _activityManager.GetCurrentActivityIfExistAsync();
            if (currentActivity == null)
            {
                return View("_Start");
            }
            else
            {
                ViewBag.BarChangeRatio = BAR_CHANE_RATIO.ToString().Replace(',', '.');
                var timeLimits = new int[] { 300, 600, 1200, 1800, 3600, 7200, 10800, 14400};                
                ViewBag.CurrTimeLimit = timeLimits.FirstOrDefault(n => n > currentActivity.Duration.TotalSeconds, (int)(currentActivity.Duration.TotalSeconds * BAR_CHANE_RATIO));

                return View(currentActivity);
            }            
        }
    }
}
