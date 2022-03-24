using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timelog.Entities;
using Timelog.Interfaces;

namespace Timelog.Repositories
{
    public class DbRepositoryActivity : DbRepositoryGeneric<UserActivityModel>, IRepositoryActivity
    {
        
        public DbRepositoryActivity(DbContext context) : base(context)
        {
        }

        public UserActivityModel getCurrentActivity()
        {
            //isUserConfigured();
            return items
                .Where(x => x.UserUniqId == UserGuid)
                .Where(a => a.Status != UserActivityModel.ActivityStatus.Complite)
                .OrderByDescending(a => a.StartTime)
                .FirstOrDefault();
        }


        //private void isUserConfigured()
        //{
        //    if (userGuid == null)
        //    {
        //        throw new Exception("the user is not configured");
        //    }
        //}
    }
}
