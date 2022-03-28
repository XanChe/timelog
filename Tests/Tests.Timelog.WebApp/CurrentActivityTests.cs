using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timelog.CoreComponent;
using Timelog.Entities;
using Timelog.Interfaces;
using Timelog.WebApp.Controllers;
using Timelog.WebApp.Models;
using Timelog.WebApp.Services;
using TimelogWebApp.Controllers;
using Xunit;

namespace Tests.Timelog.WebApp
{
    public class CurrentActivityTests
    {
        private UserManager<User> _mockUserManagerr;
        private IHttpContextAccessor _httpContextAccessor;
        public CurrentActivityTests()
        {
            _httpContextAccessor = ControllerInitHelper.GetFailContextAccessor();
            _mockUserManagerr = ControllerInitHelper.GetFailUserManager();
        }
        [Fact]
        public void IndexReturnViewResultWithStartedUserActivities()
        {
            var mockRepository = new Mock<IRepositoryActivity>();
            mockRepository.Setup(x => x.getCurrentActivity()).Returns(new UserActivityModel() { Id = 22 , Status = UserActivityModel.ActivityStatus.Started});

            var _repoManager = ControllerInitHelper.mockRepoManagerWithActivities(mockRepository.Object);
            var timelogAspService = new TimelogAspService(new TimelogComponent(_repoManager), _httpContextAccessor, _mockUserManagerr);
            var controller = new CurrentActivityController(timelogAspService);

            //Action
            var result = controller.Index();

            //Assert
            var veiwResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UserActivityModel>(veiwResult.Model);
            Assert.Equal(UserActivityModel.ActivityStatus.Started, model.Status);

        }
    }

}
