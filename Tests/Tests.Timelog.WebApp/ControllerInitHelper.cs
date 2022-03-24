using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timelog.WebApp.Models;

namespace Tests.Timelog.WebApp
{
    internal class ControllerInitHelper
    {
        public static UserManager<User>  GetFailUserManager()
        {   
            var store = new Mock<IUserStore<User>>();
            store.Setup(x => x.FindByIdAsync("4e7270f5-1a2f-470b-913f-bace90193be9", CancellationToken.None))
                .ReturnsAsync(new User()
                {
                    UserName = "test@email.com",
                    Id = "4e7270f5-1a2f-470b-913f-bace90193be9"
                });

            return new UserManager<User>(store.Object, null, null, null, null, null, null, null, null);
        }

        public static IHttpContextAccessor GetFailContextAccessor()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, "fffff@kdkd.ru"),
                new Claim(ClaimTypes.NameIdentifier, "0b47afbc-7ccf-48cb-8d40-bbbe3577acb1")
            };
            var claimsIdentity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var mockContextAcsesor = new Mock<IHttpContextAccessor>();
            mockContextAcsesor.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            return mockContextAcsesor.Object;
        }
    }
}
