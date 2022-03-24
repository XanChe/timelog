using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timelog.CoreComponent;
using Timelog.Services;
using Timelog.Entities;
using Timelog.WebApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace TimelogWebApp.Controllers
{
    [Authorize]
    public class ActivityController: Controller
    {
        private TimelogComponent timelogService;
        private UserActivityService activityManager;       
        
        public ActivityController(TimelogComponent component, UserManager<User> userManager)
        {
            timelogService = component;            
            var userUniqId = userManager.GetUserId(this.User);
            timelogService.UseUserFilter(new Guid(userUniqId));
            activityManager = timelogService.CreateUserActivityService();          
            
        }

        public IActionResult Index()
        {
            return View(activityManager.GetActivities().ToList());
        }

        [HttpPost]
        public IActionResult StartActivity(long projectId, long activityTypeId)
        {
            if (ModelState.IsValid)
            {
                var startedActivity = activityManager.StartNewActivity(projectId, activityTypeId);
                return RedirectToAction("CurrentActivity", startedActivity);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult StopActivity(UserActivityModel currentActivity)
        {
            if (ModelState.IsValid)
            {
                activityManager.StopCurrentActivityIfExist(currentActivity.Comment);
                return RedirectToAction("CurrentActivity", currentActivity);
            }
            return RedirectToAction("Index");
        }

    }
}
