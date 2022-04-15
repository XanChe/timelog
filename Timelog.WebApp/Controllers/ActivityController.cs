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
            
            return View((await _activityManager.GetActivitiesAsync()).ToList());
        }

        // GET: ActivityController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            UserActivity? activity = await _activityManager.GetByIdAsync(id);
            return View(activity);
        }
        public async Task<IActionResult> Start()
        {
            var projectManager = _timelogService.CreateProjectService();
            var activityTypeManager = _timelogService.CreateActivityTypeService();


            ViewBag.Projects = new SelectList(await projectManager.GetAllAsync(), "Id", "Name");
            ViewBag.ActivityTypes = new SelectList(await activityTypeManager.GetAllAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Start(Guid projectId, Guid activityTypeId)
        {
            if (ModelState.IsValid)
            {
                var startedActivity = await _activityManager.StartNewActivityAsync(projectId, activityTypeId);                
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Stop(string comment)
        {            
            await _activityManager.StopCurrentActivityIfExistAsync(comment ?? ""); 
            return RedirectToAction("Index");
        }

        // GET: ActivityController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            return View(await _activityManager.GetByIdAsync(id));
        }

        // POST: ActivityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _activityManager.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(await _activityManager.GetByIdAsync(id));
            }
        }

    }
}
