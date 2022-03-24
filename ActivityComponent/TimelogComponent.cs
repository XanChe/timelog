using System;
using Timelog.Entities;
using Timelog.Interfaces;
using Timelog.Services;
using Microsoft.EntityFrameworkCore;

namespace Timelog.CoreComponent
{
    public class TimelogComponent
    {
        private readonly IRepositoryManager repositoryManager;
       
        public TimelogComponent(IRepositoryManager manager)
        {
           repositoryManager = manager;
        }

        public void UseUserFilter(Guid userIdentityGuid)
        {
            repositoryManager.UseUserFilter(userIdentityGuid);
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
