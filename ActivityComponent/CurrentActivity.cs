using System;
using System.Collections.Generic;
using System.Text;
using Timelog.Entities;
using Timelog.Services.Entities;

namespace Timelog.CoreServices
{
    public class CurrentActivity 
    {
        static private CurrentActivity _instance = null;

        public UserActivity Activity{get; }
        //public User CurrentUser { get; }
        //static public UserActivity instance()
        //{
            
        //    if(_instance == null)
        //    {
        //        return null;               
                
        //    }
        //    //OneUserDatabaseContext db = DbCreator.NewUserDB(_instance.CurrentUser);
        //    //UserActivityBilder activitySrv = new UserActivityBilder(db);

        //    var dbCurActivity = activitySrv.getCurrentActivityFromDb();

        //    if (dbCurActivity != null)
        //    {
        //        _instance = new CurrentActivity(dbCurActivity);
        //    }
        //    return _instance.Activity;
        //}
        //static public UserActivity instance(User user)
        //{
        //    //OneUserDatabaseContext db = DbCreator.NewUserDB(user);
        //    //UserActivityBilder activitySrv = new UserActivityBilder(db);

        //    var dbCurActivity = activitySrv.getCurrentActivityFromDb();

        //    if (dbCurActivity != null)
        //    {
        //        _instance = new CurrentActivity(dbCurActivity);
        //    }
        //    if (_instance == null)
        //    {
                
        //        _instance = new CurrentActivity(new UserActivity(), user);               

        //    }
        //    return _instance.Activity;
        //}
        //protected CurrentActivity(UserActivity currActivity)
        //{
        //    CurrentUser = currActivity.User;
        //    Activity = currActivity;
        //}
        //protected CurrentActivity(UserActivity currActivity, User user)
        //{
        //    CurrentUser = user;
        //    currActivity.SetUser(user);
        //    Activity = currActivity;
        //}
    }
}
