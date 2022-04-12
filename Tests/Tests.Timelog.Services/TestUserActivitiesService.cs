using System;
using Xunit;
using Timelog.Services;
using Moq;
using Timelog.Core.Repositories;
using Timelog.Core.Entities;
using Timelog.Core;

namespace Tests.Timelog.Services
{
    public class TestUserActivitiesService
    {
        [Fact]
        public void UserActivityServiceIsExist()
        {
            var mockRepository = new Mock<IUnitOfWork>();
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
