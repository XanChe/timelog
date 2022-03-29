using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timelog.Services;
using Timelog.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TimelogWebApp.Controllers
{
    [Authorize]
    public class ActivityController: Controller
    {
        private TimelogServiceBuilder _timelogService;
        private UserActivityService _activityManager;

        public ActivityController(TimelogAspService timelogAspService)
        {
            _timelogService = timelogAspService.TimelogService;
            _activityManager = _timelogService.CreateUserActivityService();  
        }

        public IActionResult Index()
        {
            var projectManager = _timelogService.CreateProjectService();
            var activityTypeManager = _timelogService.CreateActivityTypeService();
                       

            ViewBag.Projects = new SelectList(projectManager.GetAll(), "Id", "Name");
            ViewBag.ActivityTypes = new SelectList(activityTypeManager.GetAll(), "Id", "Name");
            
            var activities = _activityManager.GetActivities().ToList();
            return View(activities);
        }

        // GET: ActivityController/Details/5
        public ActionResult Details(long id)
        {
            UserActivityModel activity = _activityManager.GetById(id);
            return View(activity);
        }
        public IActionResult Start()
        {
            var projectManager = _timelogService.CreateProjectService();
            var activityTypeManager = _timelogService.CreateActivityTypeService();


            ViewBag.Projects = new SelectList(projectManager.GetAll(), "Id", "Name");
            ViewBag.ActivityTypes = new SelectList(activityTypeManager.GetAll(), "Id", "Name");

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
