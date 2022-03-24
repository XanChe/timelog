using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Timelog.CoreComponent;
using Timelog.Entities;
using Timelog.Services;

namespace TimelogWebApp.Controllers
{
    public class ProjectController : Controller
    {
        private TimelogComponent timelogComponet;
        private EntityService<Project> projectService;
        private ClaimsPrincipal UserByService;

        public ProjectController(TimelogComponent component, IHttpContextAccessor contextAccessor)
        {
            timelogComponet = component;
            UserByService = contextAccessor.HttpContext.User;
            if (UserByService != null)
            {
                var userGuid = UserByService.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
                timelogComponet.UseUserFilter(new Guid(userGuid));
                projectService = timelogComponet.CreateProjectService();
            }

        }
        public IActionResult Index()
        {
            return View(projectService.GetItems());
        }

        [HttpPost]
        public IActionResult Add(Project newProject)
        {
            if (newProject != null)
            {
                projectService.Add(newProject);
                return RedirectToAction("Index");
            }
            return View(newProject);
        }
    }
}
