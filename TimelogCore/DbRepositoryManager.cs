using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timelog.Repositories;
using Timelog.Entities;
using Timelog.Interfaces;
using Timelog.EF;

namespace Timelog
{
    public class DbRepositoryManager: IRepositoryManager
    {
        private readonly DbContext _context;
       
        private readonly DbRepositoryGeneric<Project> _projectRepository;
        private readonly DbRepositoryGeneric<ActivityType> _activityTypeRepository;
        private readonly DbRepositoryActivity _activityRepository;
        public DbRepositoryManager(TimelogDbContext context)
        {
            this._context = context;                        
            _projectRepository = new DbRepositoryGeneric<Project>(_context);
            _activityTypeRepository = new DbRepositoryGeneric<ActivityType>(_context);
            _activityRepository = new DbRepositoryActivity(_context);
        }

        public IRepositoryGeneric<Project> Projects { get { return _projectRepository; } }
        public IRepositoryGeneric<ActivityType> ActivityTypes { get { return _activityTypeRepository; } }
        public IRepositoryActivity Activities { get { return _activityRepository; } }       

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UseUserFilter(Guid userIdentityGuid)
        {
            _projectRepository.SetUser(userIdentityGuid);
            _activityTypeRepository.SetUser(userIdentityGuid);
            _activityRepository.SetUser(userIdentityGuid);
        }
    }
}
