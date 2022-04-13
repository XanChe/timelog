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
    public class ProjectController : Controller
    {
        private ITimelogServiceBuilder _timelogService;
        private IEntityService<Project> _projectManager;

        public ProjectController(TimelogAspService timelogAspService)
        {
            _timelogService = timelogAspService.TimelogServiceBuilder;
            _projectManager = _timelogService.CreateProjectService();
        }

        // GET: ProjectController
        public async Task<IActionResult> Index()
        {
            return View(await _projectManager.GetAllAsync());
        }

        // GET: ProjectController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            return View(await _projectManager.GetByIdAsync(id));
        }

        // GET: ProjectController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                var project = new Project() { Name = collection["Name"], Description = collection["Description"] };
                await _projectManager.CreateAsync(project);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            
            return View(await _projectManager.GetByIdAsync(id));
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var project = new Project() { 
                    Id = id, 
                   // UniqId = new Guid(collection["UniqId"]), 
                    Name = collection["Name"], 
                    Description = collection["Description"] 
                };
                await _projectManager.UpdateAsync(project);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(await _projectManager.GetByIdAsync(id));
            }
        }

        // GET: ProjectController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            return View(await _projectManager.GetByIdAsync(id));
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _projectManager.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(await _projectManager.GetByIdAsync(id));
            }
        }
    }
}
