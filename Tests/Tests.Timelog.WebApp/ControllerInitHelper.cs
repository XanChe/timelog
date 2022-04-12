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
using Timelog.AspNetCore.Models;
using Timelog.Core;
using Timelog.Core.Entities;
using Timelog.Core.Repositories;

namespace Tests.Timelog.WebApp
{
    internal class ControllerInitHelper
    {
        private static Guid TEST_ID_1 = new Guid("b5a68909-8c42-433e-bd81-f76aa92c2166");
        private static Guid TEST_ID_2 = new Guid("b049aaf2-61c5-435e-b4a7-018c95925878");
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

        public static IUnitOfWork mockRepoManagerWithActivities(IRepositoryActivity repositoryActivity)
        {
           
            var repositoryActivityType = new Mock<IRepositoryGeneric<ActivityType>>();
            repositoryActivityType.Setup(x => x.GetAllAsync()).Returns(GetTestActivityTypes());

            var repositoryProject = new Mock<IRepositoryGeneric<Project>>();
            repositoryProject.Setup(x => x.GetAllAsync()).Returns(GetTestProjects());

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Activities).Returns(repositoryActivity);
            mock.Setup(x => x.Projects).Returns(repositoryProject.Object);
            mock.Setup(x => x.ActivityTypes).Returns(repositoryActivityType.Object);

            return mock.Object;
        }

        public static IUnitOfWork mockRepoManagerWithActivities()
        {
            var repositoryActivity = new Mock<IRepositoryActivity>();
            repositoryActivity.Setup(x => x.GetAllAsync()).Returns(GetTestActivities());

            var repositoryActivityType = new Mock<IRepositoryGeneric<ActivityType>>();
            repositoryActivityType.Setup(x => x.GetAllAsync()).Returns(GetTestActivityTypes());

            var repositoryProject = new Mock<IRepositoryGeneric<Project>>();
            repositoryProject.Setup(x => x.GetAllAsync()).Returns(GetTestProjects());

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Activities).Returns(repositoryActivity.Object);
            mock.Setup(x => x.Projects).Returns(repositoryProject.Object);
            mock.Setup(x => x.ActivityTypes).Returns(repositoryActivityType.Object);

            return mock.Object;
        }
        private static async Task<IEnumerable<UserActivity>> GetTestActivities()
        {
            return await Task.FromResult(new List<UserActivity>()
            {
                new UserActivity { Title = "First action", Status = UserActivity.ActivityStatus.Complite},
                new UserActivity { Title = "Second action", Status = UserActivity.ActivityStatus.Started}
            });
        }
        private static async Task<IEnumerable<Project>> GetTestProjects()
        {
            return await Task.FromResult(new List<Project>()
            {
                new Project { Name = "First project", Id = TEST_ID_1 },
                new Project { Name = "Second project", Id = TEST_ID_2 }
            });
        }

        private static async Task<IEnumerable<ActivityType>> GetTestActivityTypes()
        {
            return await Task.FromResult(new List<ActivityType>()
            {
                new ActivityType { Name = "First Type", Id = TEST_ID_1 },
                new ActivityType { Name = "Second Type", Id = TEST_ID_2 }
            });
        }
    }
}
