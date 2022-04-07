using Xunit;
using TimelogWebApp.Controllers;
using Timelog.Services;
using Timelog.Interfaces;
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
        public void StartGetActionExist()
        {
            var mockRepository = new Mock<IRepositoryActivity>();
            var repoManager = ControllerInitHelper.mockRepoManagerWithActivities(mockRepository.Object);

            var timelogAspService = new TimelogAspService(new TimelogServiceBuilder(repoManager), _httpContextAccessor, _mockUserManagerr);
            var controller = new ActivityController(timelogAspService);

            //Action
            var result = controller.Start();

            //Assert
            var veiwResult = Assert.IsType<ViewResult>(result);            

        }
        [Fact]
        public void IndexReturnViewResultAListOfUserActivities() 
        {
            var mockRepository = new Mock<IRepositoryActivity>();
            mockRepository.Setup(x => x.GetAll()).Returns(GetTestActivities());
            
            var _repoManager = ControllerInitHelper.mockRepoManagerWithActivities();
            var timelogAspService = new TimelogAspService(new TimelogServiceBuilder(_repoManager), _httpContextAccessor, _mockUserManagerr);
            var controller = new ActivityController(timelogAspService);            

            //Action
            var result = controller.IndexAsync();

            //Assert
            var veiwResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserActivityModel>>(veiwResult.Model);
            Assert.Equal(GetTestActivities().Count(), model.Count());

        }

        [Fact]
        public void StartActivityReturnRedirectAndStartActivity()
        {
            var mockRepository = new Mock<IRepositoryActivity>();          

            var repoManager = ControllerInitHelper.mockRepoManagerWithActivities(mockRepository.Object);

            var timelogAspService = new TimelogAspService(new TimelogServiceBuilder(repoManager), _httpContextAccessor, _mockUserManagerr);
            var controller = new ActivityController(timelogAspService);


            //Action
            var result = controller.Start(1, 1);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            //UserActivityModel model = redirectToActionResult.RouteValues;
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepository.Verify(mok => mok.Create(It.IsAny<UserActivityModel>()));

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

        

        private IEnumerable<UserActivityModel> GetTestActivities()
        {
            return new List<UserActivityModel>()
            {
                new UserActivityModel { Title = "First action", Status = UserActivityModel.ActivityStatus.Complite},
                new UserActivityModel { Title = "Second action", Status = UserActivityModel.ActivityStatus.Started}
            };
        }
        private IEnumerable<Project> GetTestPrijects()
        {
            return new List<Project>()
            {
                new Project { Name = "First action", Id = 1 },
                new Project { Name = "Second action", Id = 2 }
            };
        }


    }

}
