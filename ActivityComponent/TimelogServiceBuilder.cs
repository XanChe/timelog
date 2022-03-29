using System;
using Timelog.Entities;
using Timelog.Interfaces;

namespace Timelog.Services
{
    public class TimelogServiceBuilder
    {
        private readonly IRepositoryManager repositoryManager;
       
        public TimelogServiceBuilder(IRepositoryManager manager)
        {
           repositoryManager = manager;
        }

        public void UseUserFilter(Guid userIdentityGuid)
        {
            repositoryManager.UseUserFilter(userIdentityGuid);
        }

        public StatisticsService CreateStatisticsService()
        {
            return new StatisticsService(repositoryManager.Satistics);
        }

        public UserActivityService CreateUserActivityService()
        {            
            return new UserActivityService(repositoryManager.Activities);
        }

        public EntityService<Project> CreateProjectService()
        {
            return new EntityService<Project>(repositoryManager.Projects);
        }

        public EntityService<ActivityType> CreateActivityTypeService()
        {
            return new EntityService<ActivityType>(repositoryManager.ActivityTypes);
        }
    }
}
