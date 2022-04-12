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
using Timelog.Services;
using Timelog.WebApp.Controllers;
using Xunit;
using Microsoft.Extensions.Primitives;
using Timelog.Core.Repositories;
using Timelog.Core.Entities;
using Timelog.AspNetCore.Services;
using Timelog.Core;
using Timelog.AspNetCore.Models;

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
        public async Task IndexActionReturnListOfProjects()
        {
            var mockRepository = new Mock<IRepositoryGeneric<Project>>();
            mockRepository.Setup(x => x.GetAllAsync()).Returns(GetTestProjects());

            var repoManager = mockRepoManagerWithActivities(mockRepository.Object);
            var timelogAspService = new TimelogAspService(new TimelogServiceBuilder(repoManager), _httpContextAccessor, _mockUserManagerr);
            var controller = new ProjectController(timelogAspService);

            //Action
            var result = await controller.Index();

            //Assert
            var veiwResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Project>>(veiwResult.Model);
            Assert.Equal((await GetTestProjects()).Count(), model.Count());
        }

        [Fact]
        public async Task AddActionReturnRedirectAndAssProject()
        {
            var mockRepository = new Mock<IRepositoryGeneric<Project>>();
            var repoManager = mockRepoManagerWithActivities(mockRepository.Object);
            var timelogAspService = new TimelogAspService(new TimelogServiceBuilder(repoManager), _httpContextAccessor, _mockUserManagerr);
            var controller = new ProjectController(timelogAspService);

            var project = new Project() { Name = "Test Project", Description = "eazy" };
            var valueDictionary = new Dictionary<string, StringValues>()
            {
                { "Name", "Test Project" },
                { "Description", "eazy"}
            };
            var formItems = new FormCollection(valueDictionary);
            //Action
            var result = await controller.Create(formItems);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            mockRepository.Verify(r => r.CreateAsync(It.IsAny<Project>()));            
        }
        private IUnitOfWork mockRepoManagerWithActivities(IRepositoryGeneric<Project> repository)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.Projects).Returns(repository);

            return mock.Object;
        }

        private async Task<IEnumerable<Project>> GetTestProjects()
        {
            return await Task.FromResult(new List<Project>()
            {
                new Project { Name = "Project 1"},
                new Project { Name = "Project 2"}
            });
        }
    }
}
