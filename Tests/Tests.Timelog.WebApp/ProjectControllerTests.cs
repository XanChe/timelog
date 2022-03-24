using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Timelog.CoreComponent;
using Timelog.Entities;
using Timelog.Interfaces;
using Timelog.WebApp.Models;
using Timelog.WebApp.Services;
using TimelogWebApp.Controllers;
using Xunit;

namespace Tests.Timelog.WebApp
{
    public class ProjectControllerTests
    {


        private UserManager<User> _mockUserManagerr;
        private IHttpContextAccessor _httpContextAccessor;

        public ProjectControllerTests()
        {
            _httpContextAccessor = ControllerInitHelper.GetFailContextAccessor();
            _mockUserManagerr = ControllerInitHelper.GetFailUserManager();
        }

        [Fact]
        public void IndexActionReturnListOfProjects()
        {
            var mockRepository = new Mock<IRepositoryGeneric<Project>>();
            mockRepository.Setup(x => x.GetAll()).Returns(GetTestProjects());

            var repoManager = mockRepoManagerWithActivities(mockRepository.Object);
            var timelogAspService = new TimelogAspService(new TimelogComponent(repoManager), _httpContextAccessor, _mockUserManagerr);
            var controller = new ProjectController(timelogAspService);

            //Action
            var result = controller.Index();

            //Assert
            var veiwResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Project>>(veiwResult.Model);
            Assert.Equal(GetTestProjects().Count(), model.Count());
        }

        [Fact]
        public void AddActionReturnRedirectAndAssProject()
        {
            var mockRepository = new Mock<IRepositoryGeneric<Project>>();
            var repoManager = mockRepoManagerWithActivities(mockRepository.Object);
            var timelogAspService = new TimelogAspService(new TimelogComponent(repoManager), _httpContextAccessor, _mockUserManagerr);
            var controller = new ProjectController(timelogAspService);

            var project = new Project() { Name = "Test Project", Description = "eazy" };

            //Action
            var result = controller.Add(project);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            mockRepository.Verify(r => r.Create(project));            
        }
        private IRepositoryManager mockRepoManagerWithActivities(IRepositoryGeneric<Project> repository)
        {
            var mock = new Mock<IRepositoryManager>();
            mock.Setup(x => x.Projects).Returns(repository);

            return mock.Object;
        }

        private IEnumerable<Project> GetTestProjects()
        {
            return new List<Project>()
            {
                new Project { Name = "Project 1"},
                new Project { Name = "Project 2"}
            };
        }
    }
}
