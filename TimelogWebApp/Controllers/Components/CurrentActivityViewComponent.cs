using Microsoft.AspNetCore.Mvc;
using Timelog.AspNetCore.Services;
using Timelog.Core;
using Timelog.Core.Services;

namespace Timelog.WebApp.Controllers.Components
{
    public class CurrentActivity:ViewComponent
    {
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
                return View(currentActivity);
            }            
        }
    }
}
