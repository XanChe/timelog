using System;
using Xunit;
using Timelog.Services;
using Timelog.Interfaces;
using Timelog.Entities;
using Moq;

namespace Tests.Timelog.Services
{
    public class TestUserActivitiesService
    {
        [Fact]
        public void UserActivityServiceIsExist()
        {
            var mockRepository = new Mock<IRepositoryActivity>();
            var service = new UserActivityService(mockRepository.Object);
        }

        [Fact]
        public void EntityServiceIsExist()
        {
            var mockRepository = new Mock<IRepositoryGeneric<Project>>();
            var service = new EntityService<Project>(mockRepository.Object);
        }
    }
}
