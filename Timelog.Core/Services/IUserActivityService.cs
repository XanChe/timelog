using Timelog.Core.Entities;

namespace Timelog.Core.Services
{
    public interface IUserActivityService : IEntityService<UserActivity>
    {
        public Task StopPreviousActivityIfExist();
        public Task<UserActivity> StartNewActivity(long projectId, long activityTypeId);
        public Task<UserActivity?> GetCurrentActivityIfExist();
        public Task StopCurrentActivityIfExist(string comment);
        public Task<IEnumerable<UserActivity>> GetActivities();
    }
}
