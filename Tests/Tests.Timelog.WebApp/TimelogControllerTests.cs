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
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Tests.Timelog.WebApp
{
    public class TimelogControllerTests
    {    
        private UserManager<User> _mockUserManagerr;
        private IHttpContextAccessor _httpContextAccessor;
        public  TimelogControllerTests()
        {
            _httpContextAccessor = ControllerInitHelper.GetFailContextAccessor();
            _mockUserManagerr = ControllerInitHelper.GetFailUserManager();
        }
        [Fact]
        public void IndexReturnViewResultAListOfUserActivities() 
        {
            var mockRepository = new Mock<IRepositoryActivity>();
            mockRepository.Setup(x => x.GetAll()).Returns(GetTestActivities());
            
            var _repoManager = mockRepoManagerWithActivities(mockRepository.Object);
            var controller = new ActivityController(new TimelogComponent(_repoManager), _httpContextAccessor, _mockUserManagerr);            

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

            var controller = new ActivityController(new TimelogComponent(repoManager), _httpContextAccessor, _mockUserManagerr);


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

            var controller = new ActivityController(new TimelogComponent(repoManager), _httpContextAccessor, _mockUserManagerr);

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
