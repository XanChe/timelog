
using Timelog.Core;
using Timelog.Core.Entities;
using Timelog.Core.Repositories;
using Timelog.Core.Services;

namespace Timelog.Services
{
    public class UserActivityService: EntityService<UserActivity>, IUserActivityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserActivityService(IUnitOfWork unitOfWork) :base(unitOfWork.Activities)
        {
            _unitOfWork = unitOfWork;            
        }

        //public UserActivity GetActivityById(long Id)
        //{
        //    return _unitOfWork.Activities.Read(Id);
        //}
               
        public bool IsDraft(UserActivity activity)
        {
            return activity.Status == UserActivity.ActivityStatus.Draft;
        }

        public bool IsStarted(UserActivity activity)
        {
            return activity.Status == UserActivity.ActivityStatus.Started;
        }

        public bool IsComplite(UserActivity activity)
        {
            return activity.Status == UserActivity.ActivityStatus.Complite;
        }
        public void Start(UserActivity activity)
        {
            Start(activity, DateTime.Now);
        }
        public void Start(UserActivity activity, DateTime customStart)
        {
            if (activity.Status == UserActivity.ActivityStatus.Draft)
            {
                activity.Status = UserActivity.ActivityStatus.Started;
                activity.StartTime = customStart;
            }
        }
        public void Stop(UserActivity activity)
        {
            Stop(activity, DateTime.Now);
        }
        public void Stop(UserActivity activity, DateTime customEnd)
        {
            if (activity.Status == UserActivity.ActivityStatus.Started)
            {
                activity.Status = UserActivity.ActivityStatus.Complite;
                activity.EndTime = customEnd;
            }
        }
        public async Task StopPreviousActivityIfExistAsync()
        {
            UserActivity? currentActivity = await _unitOfWork.Activities.getCurrentActivityAsync();

            if (currentActivity != null && IsStarted(currentActivity))
            {
                Stop(currentActivity);
                await _unitOfWork.Activities.UpdateAsync(currentActivity);
                await _unitOfWork.Activities.SaveChangesAsync();
            }
        }
        public async Task<UserActivity> StartNewActivityAsync(Guid projectId, Guid activityTypeId)
        {
            await StopPreviousActivityIfExistAsync();
            var newUserActivity = new UserActivity()
            {
                ProjectId = projectId,
                ActivityTypeId = activityTypeId
            };
            
            Start(newUserActivity);

            await _unitOfWork.Activities.CreateAsync(newUserActivity);
            await _unitOfWork.Activities.SaveChangesAsync();

            return newUserActivity;
        }

        public async Task<UserActivity?> GetCurrentActivityIfExistAsync()
        {
            return await _unitOfWork.Activities.getCurrentActivityAsync();
        }

        public async Task StopCurrentActivityIfExistAsync(string comment)
        {
            UserActivity? currentActivity = await _unitOfWork.Activities.getCurrentActivityAsync();
            if (currentActivity != null)
            {
                currentActivity.Comment = comment;
                Stop(currentActivity);

                await _unitOfWork.Activities.UpdateAsync(currentActivity);
                await _unitOfWork.Activities.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserActivity>> GetActivitiesAsync()
        {          
            return await _unitOfWork.Activities.GetAllAsync();
        }
    }
}
