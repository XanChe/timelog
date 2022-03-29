using Microsoft.AspNetCore.Mvc;
using Timelog.Services;
using Timelog.Entities;

namespace Timelog.WebApp.Controllers.Components
{
    public class CurrentActivity:ViewComponent
    {
        private TimelogServiceBuilder _timelogService;
        private UserActivityService _activityManager;

        public CurrentActivity(TimelogAspService timelogAspService)
        {
            _timelogService = timelogAspService.TimelogService;
            _activityManager = _timelogService.CreateUserActivityService();
        }        
        public IViewComponentResult Invoke()
        {
            var currentActivity = (UserActivityModel)_activityManager.GetCurrentActivityIfExist();
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
