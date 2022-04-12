using Microsoft.EntityFrameworkCore;
using Timelog.Core;
using Timelog.Core.Entities;
using Timelog.Core.Repositories;
using Timelog.Data.Repositories;

namespace Timelog.Data
{
    public class DbUnitOfWork: IUnitOfWork
    {
        private readonly DbContext _context;
       
        private readonly DbRepositoryGeneric<Project> _projectRepository;
        private readonly DbRepositoryGeneric<ActivityType> _activityTypeRepository;
        private readonly DbRepositoryActivity _activityRepository;
        private readonly DbRepositiryStatistics _statisticsRepository;
        public DbUnitOfWork(TimelogDbContext context)
        {
            this._context = context;                        
            _projectRepository = new DbRepositoryGeneric<Project>(context);
            _activityTypeRepository = new DbRepositoryGeneric<ActivityType>(context);
            _activityRepository = new DbRepositoryActivity(context);
            _statisticsRepository = new DbRepositiryStatistics(context);
        }

        public IRepositoryGeneric<Project> Projects { get { return _projectRepository; } }
        public IRepositoryGeneric<ActivityType> ActivityTypes { get { return _activityTypeRepository; } }
        public IRepositoryActivity Activities { get { return _activityRepository; } }
        public IRepositirySatistics Satistics { get { return _statisticsRepository; } }

        public void Dispose()
        {
            _context.Dispose();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UseUserFilter(Guid userIdentityGuid)
        {
            _projectRepository.SetUser(userIdentityGuid);
            _activityTypeRepository.SetUser(userIdentityGuid);
            _activityRepository.SetUser(userIdentityGuid);
            _statisticsRepository.SetUser(userIdentityGuid);
        }
    }
}
