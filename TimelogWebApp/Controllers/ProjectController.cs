using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Timelog.CoreComponent;
using Timelog.Entities;
using Timelog.Services;
using Timelog.WebApp.Services;

namespace TimelogWebApp.Controllers
{
    public class ProjectController : Controller
    {
        private TimelogComponent _timelogService;
        private EntityService<Project> _projectManager;

        public ProjectController(TimelogAspService timelogAspService)
        {
            _timelogService = timelogAspService.TimelogService;
            _projectManager = _timelogService.CreateProjectService();
        }
        public IActionResult Index()
        {
            return View(_projectManager.GetItems());
        }

        [HttpPost]
        public IActionResult Add(Project newProject)
        {
            if (newProject != null)
            {
                _projectManager.Add(newProject);
                return RedirectToAction("Index");
            }
            return View(newProject);
        }
    }
}
