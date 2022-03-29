using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Timelog.Services;
using Timelog.WebApp.Models;

namespace Timelog.Services
{
    public class TimelogAspService
    {
        private TimelogServiceBuilder _timelogService;
        public TimelogServiceBuilder TimelogService { get { return _timelogService; } }
        public TimelogAspService(TimelogServiceBuilder component, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {            
            if (contextAccessor == null)
            {
                throw new ArgumentNullException(nameof(contextAccessor));
            }
            _timelogService = component;
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            ClaimsPrincipal userClaims = contextAccessor.HttpContext.User;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
            var userUniqId = userManager.GetUserId(userClaims);
            _timelogService.UseUserFilter(new Guid(userUniqId));            
        }
    }
}
