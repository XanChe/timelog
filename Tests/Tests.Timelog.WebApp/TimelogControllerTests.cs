using Xunit;
using TimelogWebApp.Controllers;
using Timelog.CoreComponent;
using Timelog.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Timelog.WebApp.Models;
using Timelog.Entities;

namespace Tests.Timelog.WebApp
{
    public class TimelogControllerTests
    {    
        private UserManager<User> _userManagerr;

        public TimelogControllerTests()
        {
            //timelogService = new TimelogComponent(options => options.UseNpgsql("Host=localhost;Port=5432;Database=test_timelog_db2;Username=tester;Password=formatmeplz"));
            /*var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, "fffff@kdkd.ru"),
                new Claim(ClaimTypes.NameIdentifier, "0b47afbc-7ccf-48cb-8d40-bbbe3577acb1")
            };
            claimsIdentity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);*/

            var mockUserManager = new Mock<UserManager<User>>();

            mockUserManager.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("4e7270f5-1a2f-470b-913f-bace90193be9");

            _userManagerr = mockUserManager.Object;


        }
        [Fact]
        public void IndexReturnViewResultAListOfUserActivities() 
        {
            var mockRepository = new Mock<IRepositoryActivity>();
            mockRepository.Setup(x => x.GetAll()).Returns(GetTestActivities());
            
            var _repoManager = mockRepoManagerWithActivities(mockRepository.Object);
            var controller = new ActivityController(new TimelogComponent(_repoManager), _userManagerr);

            //Action
            var result = controller.Index();

            //Assert
            var veiwResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserActivityModel>>(veiwResult.Model);
            Assert.Equal(GetTestActivities().Count(), model.Count());

        }

        [Fact]
        public void StartActivityReturnRedirectAndStartActivity()
        {
            var mockRepository = new Mock<IRepositoryActivity>();          

            var repoManager = mockRepoManagerWithActivities(mockRepository.Object);

            var controller = new ActivityController(new TimelogComponent(repoManager), _userManagerr);


            //Action
            var result = controller.StartActivity(1, 1);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            //UserActivityModel model = redirectToActionResult.RouteValues;
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("CurrentActivity", redirectToActionResult.ActionName);
            mockRepository.VerifyAll();

        }
        [Fact]
        public void StopActivityReturnRedirectAndStopActivity()
        {
            var mockRepository = new Mock<IRepositoryActivity>();

            var repoManager = mockRepoManagerWithActivities(mockRepository.Object);

            var controller = new ActivityController(new TimelogComponent(repoManager), _userManagerr);

            var activity = new UserActivityModel()
            {
                Title = "Активная деаятельность",
                Id = 1,
                StartTime = DateTime.Now.AddHours(-1),
                Status = UserActivityModel.ActivityStatus.Started
            };

            //Action
            var result = controller.StopActivity(activity);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("CurrentActivity", redirectToActionResult.ActionName);
        }

        private IRepositoryManager mockRepoManagerWithActivities(IRepositoryActivity repositoryActivity)
        {
            var mock = new Mock<IRepositoryManager>();
            mock.Setup(x => x.Activities).Returns(repositoryActivity);

            return mock.Object;
        }

        private IEnumerable<UserActivityModel> GetTestActivities()
        {
            return new List<UserActivityModel>()
            {
                new UserActivityModel { Title = "First action", Status = UserActivityModel.ActivityStatus.Complite},
                new UserActivityModel { Title = "Second action", Status = UserActivityModel.ActivityStatus.Started}
            };
        }


    }

}
