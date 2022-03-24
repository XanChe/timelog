using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Timelog.CoreComponent;
using Timelog.WebApp.Models;

namespace Timelog.WebApp.Services
{
    public class TimelogAspService
    {
        private TimelogComponent _timelogService;
        public TimelogComponent TimelogService { get { return _timelogService; } }
        public TimelogAspService(TimelogComponent component, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {            
            if (contextAccessor == null)
            {
                throw new ArgumentNullException(nameof(contextAccessor));
            }
            _timelogService = component;
            ClaimsPrincipal userClaims = contextAccessor.HttpContext.User;
            var userUniqId = userManager.GetUserId(userClaims);
            _timelogService.UseUserFilter(new Guid(userUniqId));            
        }
    }
}
