using AutoFixture;
using Xunit;
using Timelog.Services;
using Moq;
using System.Linq;
using Timelog.Core.Repositories;
using Timelog.Data.Repositories;
using Timelog.Core;
using Timelog.Core.Entities;
using Timelog.Data;
using System.Threading.Tasks;
using System;
using Timelog.Core.ViewModels;

namespace Tests.Timelog.Component
{
    public class UserActivityManagerTests
    {
        private static readonly Fixture Fixture = new Fixture();

        private IUnitOfWork _unitOfWork;
        private IRepositoryActivity _inMemoryRepository;
       
        public UserActivityManagerTests()
        {
            
            var testContext = new SqliteInMemoryContext().TestContext;
            _unitOfWork = new DbUnitOfWork(testContext);

            _inMemoryRepository = new DbRepositoryActivity(testContext);
        }

        [Fact]
        public void UserActivityManagerTestCreater()
        {
           
            
            var activityManager = new UserActivityService(_unitOfWork);
            Assert.NotNull(activityManager);
        }

        [Fact]
        public void GetActivityByIdTest()
        {
            var activityManager = createUserActivityManagerWithPremadeMoqUserActivities();
            //Action
            var activity = activityManager.GetByIdAsync(new Guid("e1067f3b-ebcb-4b4e-8f91-5fce1593933f"));
            //Assert
            Assert.NotNull(activity);

        }
        // несовместимость с SqlLite
       /* [Fact]
        public async Task StartNewActivityTest()
        {
            var activityType = new ActivityType() { Id = Guid.NewGuid(), Name = "tt" };
            var activityProject = new Project() { Id = Guid.NewGuid(), Name = "test p" };
            _unitOfWork.Activities.Create(new UserActivity() { ActivityType = activityType, Project = activityProject });
            await _unitOfWork.SaveChangesAsync();
            var activityManager = new UserActivityService(_unitOfWork);

            await activityManager.StartNewActivityAsync(activityType.Id, activityProject.Id);

            var openedActivities = activityManager.GetCurrentActivityIfExistAsync();               

            Assert.NotNull(openedActivities);
        }*/

        [Fact]        
        public async Task StopPreviousActivityIfExistTest()
        {
            var activityManager = createUserActivityManagerWithPremadeMoqUserActivities();

            //Action
            var openedActivitiesBefoAction = activityManager.GetCurrentActivityIfExistAsync();
            //Assert
            Assert.NotNull(openedActivitiesBefoAction);
            //Action
            await activityManager.StopPreviousActivityIfExistAsync();
            //Assert
            var openedActivities = await activityManager.GetCurrentActivityIfExistAsync();
            Assert.Null(openedActivities);
        }


        [Fact]
        public void GetCurrentActivityIfExistTest()
        {
            var activityManager = createUserActivityManagerWithPremadeMoqUserActivities();
            //Action
            var currentActivity = activityManager.GetCurrentActivityIfExistAsync();
            //Assert
            Assert.NotNull(currentActivity);
        }

        [Fact]
        public async Task StopCurrentActivityIfExistTest()
        {
            var activityManager = createUserActivityManagerWithPremadeMoqUserActivities();

            var currentActivity = await activityManager.GetCurrentActivityIfExistAsync();
            //Action
            await activityManager.StopCurrentActivityIfExistAsync("Tested activity");

            //Assert
            ActivityViewModel lastActivity = (await activityManager.GetActivitiesAsync()).Where(a => a.Id == currentActivity.Id).FirstOrDefault();
            Assert.True(activityManager.IsComplite(lastActivity.MapToUserActivity()));
            Assert.Equal("Tested activity", lastActivity.Comment);
        }

        private UserActivityService createUserActivityManagerWithPremadeMoqUserActivities()
        {
            var project = new Project() { Id = Guid.NewGuid(), Name = "nn" };
            var activityType = new ActivityType() { Id = Guid.NewGuid(), Name = "tt" };
            _unitOfWork.Activities.Create(new UserActivity { Id = new Guid("e1067f3b-ebcb-4b4e-8f91-5fce1593933f"), Status = ActivityStatus.Complite, Project = project, ActivityType = activityType });
            _unitOfWork.Activities.Create(new UserActivity {  Status = ActivityStatus.Complite, Project = project, ActivityType = activityType });

            _unitOfWork.Activities.Create(new UserActivity {  Status = ActivityStatus.Started, Project = project, ActivityType = activityType });
            _unitOfWork.SaveChangesAsync();

            return new UserActivityService(_unitOfWork);
        }

       
    }
}
