using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timelog.CoreComponent;
using Timelog.Entities;
using Timelog.Services;
using Timelog.WebApp.Services;

namespace Timelog.WebApp.Controllers
{
    [Authorize]
    public class ActivityTypeController : Controller
    {
        private TimelogComponent _timelogService;
        private EntityService<ActivityType> _activityTypeManager;

        public ActivityTypeController(TimelogAspService timelogAspService)
        {
            _timelogService = timelogAspService.TimelogService;
            _activityTypeManager = _timelogService.CreateActivityTypeService();
        }
        // GET: ActivityTypeController
        public ActionResult Index()
        {
            return View(_activityTypeManager.GetAll());
        }

        // GET: ActivityTypeController/Details/5
        public ActionResult Details(long id)
        {
            return View(_activityTypeManager.GetById(id));
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

                _activityTypeManager.Create(activityType);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActivityTypeController/Edit/5
        public ActionResult Edit(long id)
        {
            
            return View(_activityTypeManager.GetById(id));
        }

        // POST: ActivityTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, IFormCollection collection)
        {
            try
            {
                var activityType = new ActivityType() { 
                    Id = id,
                    UniqId = new Guid(collection["UniqId"]),
                    Name = collection["Name"], 
                    Description = collection["Description"] 
                };
                _activityTypeManager.Update(activityType);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_activityTypeManager.GetById(id));
            }
        }

        // GET: ActivityTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_activityTypeManager.GetById(id));
        }

        // POST: ActivityTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id, IFormCollection collection)
        {
            try
            {
                _activityTypeManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_activityTypeManager.GetById(id));
            }
        }
    }
}
