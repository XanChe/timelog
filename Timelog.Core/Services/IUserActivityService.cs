using Timelog.Core.Entities;

namespace Timelog.Core.Services
{
    public interface IUserActivityService : IEntityService<UserActivity>
    {
        public Task StopPreviousActivityIfExistAsync();
        public Task<UserActivity> StartNewActivityAsync(Guid projectId, Guid activityTypeId);
        public Task<UserActivity?> GetCurrentActivityIfExistAsync();
        public Task StopCurrentActivityIfExistAsync(string comment);
        public Task<IEnumerable<UserActivity>> GetActivitiesAsync();
    }
}
