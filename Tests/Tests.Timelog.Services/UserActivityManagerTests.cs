using AutoFixture;
using System;
using System.Collections.Generic;
using Xunit;
using Timelog.CoreComponent;
using Timelog.Interfaces;
using Timelog.Entities;
using Timelog.Repositories;
using Timelog.Services.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Timelog.Services;

namespace Tests.Timelog.Component
{
    public class UserActivityManagerTests
    {
        private static readonly Fixture Fixture = new Fixture();

        private Mock<IRepositoryActivity> _moqRepository;
        private IRepositoryActivity _inMemoryRepository;
       
        public UserActivityManagerTests()
        {
            _moqRepository = new Mock<IRepositoryActivity>();
            var testContext = new SqliteInMemoryContext().TestContext;

            _inMemoryRepository = new DbRepositoryActivity(testContext);
        }

        [Fact]
        public void UserActivityManagerTestCreater()
        {
           
            
            var activityManager = new UserActivityService(_moqRepository.Object);
            Assert.NotNull(activityManager);
        }

        [Fact]
        public void GetActivityByIdTest()
        {
            var activityManager = createUserActivityManagerWithPremadeMoqUserActivities();
            //Action
            var activity = activityManager.GetById(1);
            //Assert
            Assert.NotNull(activity);

        }

        [Fact]
        public void StartNewActivityTest()
        {
            var activityType = new ActivityType() { Id = 1, Name = "tt" };
            _inMemoryRepository.Create(new UserActivityModel() { ActivityType = activityType, Project = new Project() { Id = 1 } });
            _inMemoryRepository.SaveChanges();
            var activityManager = new UserActivityService(_inMemoryRepository);

            activityManager.StartNewActivity(1, 1);

            var openedActivities = activityManager.GetCurrentActivityIfExist();               

            Assert.NotNull(openedActivities);
        }

        [Fact]        
        public void StopPreviousActivityIfExistTest()
        {
            var activityManager = createUserActivityManagerWithPremadeMoqUserActivities();

            //Action
            var openedActivitiesBefoAction = activityManager.GetCurrentActivityIfExist();
            //Assert
            Assert.NotNull(openedActivitiesBefoAction);
            //Action
            activityManager.StopPreviousActivityIfExist();
            //Assert
            var openedActivities = activityManager.GetCurrentActivityIfExist();
            Assert.Null(openedActivities);
        }


        [Fact]
        public void GetCurrentActivityIfExistTest()
        {
            var activityManager = createUserActivityManagerWithPremadeMoqUserActivities();
            //Action
            var currentActivity = activityManager.GetCurrentActivityIfExist();
            //Assert
            Assert.NotNull(currentActivity);
        }

        [Fact]
        public void StopCurrentActivityIfExistTest()
        {
            var activityManager = createUserActivityManagerWithPremadeMoqUserActivities();

            var currentActivity = activityManager.GetCurrentActivityIfExist();
            //Action
            activityManager.StopCurrentActivityIfExist("Tested activity");

            //Assert
            UserActivity lastActivity = activityManager.GetActivities().Where(a => a.UniqId.ToString() == currentActivity.UniqId).FirstOrDefault();
            Assert.True(lastActivity.IsComplite());
            Assert.Equal("Tested activity", lastActivity.Comment);
        }

        private UserActivityService createUserActivityManagerWithPremadeMoqUserActivities()
        {
            var project = new Project() { Id = 1, Name = "nn" };
            var activityType = new ActivityType() { Id = 1, Name = "tt" };
            _inMemoryRepository.Create(new UserActivityModel {  Status = UserActivityModel.ActivityStatus.Complite, Project = project, ActivityType = activityType });
            _inMemoryRepository.Create(new UserActivityModel {  Status = UserActivityModel.ActivityStatus.Complite, Project = project, ActivityType = activityType });

            _inMemoryRepository.Create(new UserActivityModel {  Status = UserActivityModel.ActivityStatus.Started, Project = project, ActivityType = activityType });
            _inMemoryRepository.SaveChanges();

            return new UserActivityService(_inMemoryRepository);
        }

       
    }
}
