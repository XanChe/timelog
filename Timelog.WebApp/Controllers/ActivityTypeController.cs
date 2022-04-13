using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timelog.AspNetCore.Services;
using Timelog.Core;
using Timelog.Core.Entities;
using Timelog.Core.Services;
using Timelog.Services;

namespace Timelog.WebApp.Controllers
{
    [Authorize]
    public class ActivityTypeController : Controller
    {
        private ITimelogServiceBuilder _timelogServiceBuilder;
        private IEntityService<ActivityType> _activityTypeManager;

        public ActivityTypeController(TimelogAspService timelogAspService)
        {
            _timelogServiceBuilder = timelogAspService.TimelogServiceBuilder;
            _activityTypeManager = _timelogServiceBuilder.CreateActivityTypeService();
        }
        // GET: ActivityTypeController
        public ActionResult Index()
        {
            return View(_activityTypeManager.GetAllAsync());
        }

        // GET: ActivityTypeController/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_activityTypeManager.GetByIdAsync(id));
        }

        // GET: ActivityTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var activityType = new ActivityType() { Name = collection["Name"], Description = collection["Description"] };

                _activityTypeManager.CreateAsync(activityType);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActivityTypeController/Edit/5
        public ActionResult Edit(Guid id)
        {
            
            return View(_activityTypeManager.GetByIdAsync(id));
        }

        // POST: ActivityTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var activityType = new ActivityType() { 
                    Id = id,
                   // UniqId = new Guid(collection["UniqId"]),
                    Name = collection["Name"], 
                    Description = collection["Description"] 
                };
                _activityTypeManager.UpdateAsync(activityType);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_activityTypeManager.GetByIdAsync(id));
            }
        }

        // GET: ActivityTypeController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(_activityTypeManager.GetByIdAsync(id));
        }

        // POST: ActivityTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _activityTypeManager.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_activityTypeManager.GetByIdAsync(id));
            }
        }
    }
}
