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
using Timelog.Entities;
using Timelog.Interfaces;
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

        public static IRepositoryManager mockRepoManagerWithActivities(IRepositoryActivity repositoryActivity)
        {
           
            var repositoryActivityType = new Mock<IRepositoryGeneric<ActivityType>>();
            repositoryActivityType.Setup(x => x.GetAll()).Returns(GetTestActivityTypes());

            var repositoryProject = new Mock<IRepositoryGeneric<Project>>();
            repositoryProject.Setup(x => x.GetAll()).Returns(GetTestProjects());

            var mock = new Mock<IRepositoryManager>();
            mock.Setup(x => x.Activities).Returns(repositoryActivity);
            mock.Setup(x => x.Projects).Returns(repositoryProject.Object);
            mock.Setup(x => x.ActivityTypes).Returns(repositoryActivityType.Object);

            return mock.Object;
        }

        public static IRepositoryManager mockRepoManagerWithActivities()
        {
            var repositoryActivity = new Mock<IRepositoryActivity>();
            repositoryActivity.Setup(x => x.GetAll()).Returns(GetTestActivities());

            var repositoryActivityType = new Mock<IRepositoryGeneric<ActivityType>>();
            repositoryActivityType.Setup(x => x.GetAll()).Returns(GetTestActivityTypes());

            var repositoryProject = new Mock<IRepositoryGeneric<Project>>();
            repositoryProject.Setup(x => x.GetAll()).Returns(GetTestProjects());

            var mock = new Mock<IRepositoryManager>();
            mock.Setup(x => x.Activities).Returns(repositoryActivity.Object);
            mock.Setup(x => x.Projects).Returns(repositoryProject.Object);
            mock.Setup(x => x.ActivityTypes).Returns(repositoryActivityType.Object);

            return mock.Object;
        }
        private static IEnumerable<UserActivityModel> GetTestActivities()
        {
            return new List<UserActivityModel>()
            {
                new UserActivityModel { Title = "First action", Status = UserActivityModel.ActivityStatus.Complite},
                new UserActivityModel { Title = "Second action", Status = UserActivityModel.ActivityStatus.Started}
            };
        }
        private static IEnumerable<Project> GetTestProjects()
        {
            return new List<Project>()
            {
                new Project { Name = "First project", Id = 1 },
                new Project { Name = "Second project", Id = 2 }
            };
        }

        private static IEnumerable<ActivityType> GetTestActivityTypes()
        {
            return new List<ActivityType>()
            {
                new ActivityType { Name = "First Type", Id = 1 },
                new ActivityType { Name = "Second Type", Id = 2 }
            };
        }
    }
}
