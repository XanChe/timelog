using Xunit;
using TimelogWebApp.Controllers;
using Timelog.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Timelog.Core.Repositories;
using Timelog.AspNetCore.Services;
using Timelog.Core.Entities;
using Timelog.AspNetCore.Models;
using System.Threading.Tasks;

namespace Tests.Timelog.WebApp
{
    public class TimelogControllerTests
    {
        private static Guid TEST_ID_1 = new Guid("b5a68909-8c42-433e-bd81-f76aa92c2166");
        private static Guid TEST_ID_2 = new Guid("b049aaf2-61c5-435e-b4a7-018c95925878");

        private UserManager<User> _mockUserManagerr;
        private IHttpContextAccessor _httpContextAccessor;
        public  TimelogControllerTests()
        {
            _httpContextAccessor = ControllerInitHelper.GetFailContextAccessor();
            _mockUserManagerr = ControllerInitHelper.GetFailUserManager();
        }
        
        [Fact]
        public async Task StartGetActionExist()
        {
            var mockRepository = new Mock<IRepositoryActivity>();
            var repoManager = ControllerInitHelper.mockRepoManagerWithActivities(mockRepository.Object);

            var timelogAspService = new TimelogAspService(new TimelogServiceBuilder(repoManager), _httpContextAccessor, _mockUserManagerr);
            var controller = new ActivityController(timelogAspService);

            //Action
            var result = await controller.Start();

            //Assert
            var veiwResult = Assert.IsType<ViewResult>(result);            

        }
        [Fact]
        public async Task IndexReturnViewResultAListOfUserActivities() 
        {
            var mockRepository = new Mock<IRepositoryActivity>();
            mockRepository.Setup(x => x.GetAllAsync()).Returns(GetTestActivities());
            
            var _repoManager = ControllerInitHelper.mockRepoManagerWithActivities();
            var timelogAspService = new TimelogAspService(new TimelogServiceBuilder(_repoManager), _httpContextAccessor, _mockUserManagerr);
            var controller = new ActivityController(timelogAspService);            

            //Action
            var result = await controller.Index();

            //Assert
            var veiwResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserActivity>>(veiwResult.Model);
            Assert.Equal((await GetTestActivities()).Count(), model.Count());

        }

        [Fact]
        public void StartActivityReturnRedirectAndStartActivity()
        {
            var mockRepository = new Mock<IRepositoryActivity>();          

            var repoManager = ControllerInitHelper.mockRepoManagerWithActivities(mockRepository.Object);

            var timelogAspService = new TimelogAspService(new TimelogServiceBuilder(repoManager), _httpContextAccessor, _mockUserManagerr);
            var controller = new ActivityController(timelogAspService);


            //Action
            var result = controller.Start(TEST_ID_1, TEST_ID_2);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            //UserActivityModel model = redirectToActionResult.RouteValues;
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepository.Verify(mok => mok.CreateAsync(It.IsAny<UserActivity>()));

        }
        [Fact]
        public void StopActivityReturnRedirectAndStopActivity()
        {
            var mockRepository = new Mock<IRepositoryActivity>();

            var repoManager = ControllerInitHelper.mockRepoManagerWithActivities(mockRepository.Object);

            var timelogAspService = new TimelogAspService(new TimelogServiceBuilder(repoManager), _httpContextAccessor, _mockUserManagerr);
            var controller = new ActivityController(timelogAspService);
            //Action
            var result = controller.Stop("Stop Comment");
            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        

        private async Task<IEnumerable<UserActivity>> GetTestActivities()
        {
            return await Task.FromResult(new List<UserActivity>()
            {
                new UserActivity { Title = "First action", Status = UserActivity.ActivityStatus.Complite},
                new UserActivity { Title = "Second action", Status = UserActivity.ActivityStatus.Started}
            }.ToList());
        }
        private IEnumerable<Project> GetTestPrijects()
        {
            return new List<Project>()
            {
                new Project { Name = "First action", Id = TEST_ID_1 },
                new Project { Name = "Second action", Id = TEST_ID_2 }
            };
        }


    }

}
