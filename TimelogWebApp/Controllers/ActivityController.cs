using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timelog.AspNetCore.Services;
using Timelog.Core.Services;
using Timelog.Core.Entities;
using Timelog.Core;

namespace TimelogWebApp.Controllers
{
    [Authorize]
    public class ActivityController: Controller
    {
        private ITimelogServiceBuilder _timelogService;
        private IUserActivityService _activityManager;

        public ActivityController(TimelogAspService timelogAspService)
        {
            _timelogService = timelogAspService.TimelogServiceBuilder;
            _activityManager = _timelogService.CreateUserActivityService();  
        }

        public async Task<IActionResult> Index()
        {
            var projectManager = _timelogService.CreateProjectService();
            var activityTypeManager = _timelogService.CreateActivityTypeService();
                       

            ViewBag.Projects = new SelectList(await projectManager.GetAllAsync(), "Id", "Name");
            ViewBag.ActivityTypes = new SelectList(await activityTypeManager.GetAllAsync(), "Id", "Name");
            
            //var activities = _activityManager.GetActivities().ToList();
            return View(await _activityManager.GetActivities());
        }

        // GET: ActivityController/Details/5
        public async Task<ActionResult> Details(long id)
        {
            UserActivity activity = await _activityManager.GetByIdAsync(id);
            return View(activity);
        }
        public IActionResult Start()
        {
            var projectManager = _timelogService.CreateProjectService();
            var activityTypeManager = _timelogService.CreateActivityTypeService();


            ViewBag.Projects = new SelectList(projectManager.GetAllAsync(), "Id", "Name");
            ViewBag.ActivityTypes = new SelectList(activityTypeManager.GetAllAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Start(long projectId, long activityTypeId)
        {
            if (ModelState.IsValid)
            {
                var startedActivity = _activityManager.StartNewActivity(projectId, activityTypeId);                
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Stop(string comment)
        {            
            _activityManager.StopCurrentActivityIfExist(comment); 
            return RedirectToAction("Index");
        }

    }
}
