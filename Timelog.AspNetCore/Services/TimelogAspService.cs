using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Timelog.AspNetCore.Models;
using Timelog.Core;
using Timelog.Services;


namespace Timelog.AspNetCore.Services
{
    public class TimelogAspService
    {
        private ITimelogServiceBuilder _timelogService;
        public ITimelogServiceBuilder TimelogServiceBuilder { get { return _timelogService; } }
        public TimelogAspService(ITimelogServiceBuilder component, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
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
