using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timelog.CoreComponent;
using Timelog.Entities;
using Timelog.Services;
using Timelog.WebApp.Services;

namespace Timelog.WebApp.Controllers
{
    public class CurrentActivityController : Controller
    {

        private TimelogComponent _timelogService;
        private UserActivityService _activityManager;

        public CurrentActivityController(TimelogAspService timelogAspService)
        {
            _timelogService = timelogAspService.TimelogService;
            _activityManager = _timelogService.CreateUserActivityService();
        }
        // GET: CurrentActivityController
        public ActionResult Index()
        {
            UserActivityModel activity = _activityManager.GetCurrentActivityIfExist();
            if (activity != null)
            {
                return View(activity);
            }
            return RedirectToAction("Start", "Activity");
        }

        // GET: CurrentActivityController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CurrentActivityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CurrentActivityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CurrentActivityController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CurrentActivityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CurrentActivityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CurrentActivityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
