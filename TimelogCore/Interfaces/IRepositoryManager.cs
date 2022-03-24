using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timelog.Entities;

namespace Timelog.Interfaces
{
    public interface IRepositoryManager: IDisposable

    {       
        IRepositoryGeneric<ActivityType> ActivityTypes { get; }
        IRepositoryGeneric<Project> Projects { get; }
        IRepositoryActivity Activities { get; }
        void UseUserFilter(Guid userIdentityGuid);
        void SaveChanges();
    }
}
