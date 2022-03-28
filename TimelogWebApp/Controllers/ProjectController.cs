using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timelog.CoreComponent;
using Timelog.Entities;
using Timelog.Services;
using Timelog.WebApp.Services;

namespace Timelog.WebApp.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private TimelogComponent _timelogService;
        private EntityService<Project> _projectManager;

        public ProjectController(TimelogAspService timelogAspService)
        {
            _timelogService = timelogAspService.TimelogService;
            _projectManager = _timelogService.CreateProjectService();
        }

        // GET: ProjectController
        public IActionResult Index()
        {
            return View(_projectManager.GetAll());
        }

        // GET: ProjectController/Details/5
        public IActionResult Details(long id)
        {
            return View(_projectManager.GetById(id));
        }

        // GET: ProjectController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                var project = new Project() { Name = collection["Name"], Description = collection["Description"] };
                _projectManager.Create(project);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Edit/5
        public IActionResult Edit(long id)
        {
            
            return View(_projectManager.GetById(id));
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, IFormCollection collection)
        {
            try
            {
                var project = new Project() { 
                    Id = id, 
                    UniqId = new Guid(collection["UniqId"]), 
                    Name = collection["Name"], 
                    Description = collection["Description"] 
                };
                _projectManager.Update(project);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_projectManager.GetById(id));
            }
        }

        // GET: ProjectController/Delete/5
        public IActionResult Delete(long id)
        {
            return View(_projectManager.GetById(id));
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id, IFormCollection collection)
        {
            try
            {
                _projectManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_projectManager.GetById(id));
            }
        }
    }
}
