
using System.Collections.Generic;
using Timelog.Interfaces;
using Timelog.Services.Entities;
using Timelog.Entities;

namespace Timelog.Services
{
    public class UserActivityService
    {
        private IRepositoryActivity _repository;
       
        public UserActivityService(IRepositoryActivity repository)
        {            
            _repository = repository;            
        }
        public void StopPreviousActivityIfExist()
        {
            UserActivity currentActivity = _repository.getCurrentActivity();

            if (currentActivity != null && currentActivity.IsStarted())
            {
                currentActivity.Stop();
                _repository.Update(currentActivity);
                _repository.SaveChanges();
            }
        }
        public UserActivity StartNewActivity(long projectId, long activityTypeId)
        {
            StopPreviousActivityIfExist();
            var newUserActivity = new UserActivity();
            newUserActivity.SetProject(projectId);
            newUserActivity.SetType(activityTypeId);
            newUserActivity.Start();
            _repository.Create(newUserActivity);
            _repository.SaveChanges();

            return newUserActivity;
        }

        public UserActivity GetCurrentActivityIfExist()
        {
            return _repository.getCurrentActivity();
        }

        public void StopCurrentActivityIfExist(string comment)
        {
            UserActivity currentActivity = _repository.getCurrentActivity();
            if (currentActivity != null)
            {
                currentActivity.Comment = comment;
                currentActivity.Stop();
            }
        }

        public IEnumerable<UserActivityModel> GetActivities()
        {          
            return _repository.GetAll();
        }
    }
}
