using System;
using System.Collections.Generic;
using System.Linq;
using Timelog.Entities;
using Timelog.Interfaces;
using Timelog.Services.Entities;

namespace Timelog.Services
{
    public class UserActivityBilder
    {
        private enum SaverStyle
        {
            SINGLE_SAVE,
            MULTI_SAVE
        }
        private IRepositoryActivity _repositoryActivity;        

        //private SaverStyle databaseSaverStyle = SaverStyle.SINGLE_SAVE;

        public UserActivityBilder(IRepositoryActivity repository)
        {

            this._repositoryActivity = repository;
        }
        //public UserActivity CreateUserActivity()
        //{
        //    return CreateUserActivity(new UserActivity());
        //}

        //public UserActivity CreateUserActivity(UserActivity userActivity)
        //{
        //    userActivity.UserUniqId = _repositoryActivity.UserGuid;
        //    userActivity.SetBilder(this);
        //    return userActivity;
        //}

        //public UserActivity openUserActivityById(long id)
        //{
        //    return CreateUserActivity(this.currentUserDatabaseContext.UserActivities.Where(u => u.Id == id).SingleOrDefault());
        //}

        //public UserActivity getCurrentActivityFromDb()
        //{
        //    UserActivity userActivity = currentUserDatabaseContext.UserActivities
        //        .Where(a => a.Status != UserActivityModel.ActivityStatus.Complite)
        //        .OrderByDescending(a => a.StartTime)
        //        .FirstOrDefault();

        //    userActivity?.SetBilder(this);
        //    return userActivity;
        //}
        //public DateTime calcActivityEndTime(UserActivity userActivity, int maxDurationMinutes)
        //{
        //    UserActivity nextActivity = currentUserDatabaseContext.UserActivities
        //        .Where(a => a.StartTime > userActivity.StartTime)
        //        .OrderBy(a => a.StartTime)
        //        .FirstOrDefault();

        //    if (nextActivity != null)
        //    {
        //        Double currrentDuration = nextActivity.StartTime.Subtract(userActivity.StartTime).TotalMinutes - 1;
        //        currrentDuration = (currrentDuration < maxDurationMinutes) ? currrentDuration : maxDurationMinutes;
        //        return userActivity.StartTime.AddMinutes(currrentDuration);
        //    }
        //    return userActivity.StartTime.AddMinutes(maxDurationMinutes);
        //}



        //public int StopOpennedActivities(int maxDurationMinutes = 240)
        //{
        //    var opennedActivities = GetStartedActivitiesWhoOlderWhen(maxDurationMinutes);

        //    StartDtabeseMultiChanges();
        //    int cnt = 0;
        //    foreach (UserActivity openedActivity in opennedActivities)
        //    {
        //        CreateUserActivity(openedActivity).Stop(calcActivityEndTime(openedActivity, maxDurationMinutes));
        //        cnt++;
        //    }
        //    SaveDatabaseContextChanges();
        //    return cnt;
        //}

        //public override void SaveUserActivity(UserActivityModel theUserActivity)
        //{
        //    currentUserDatabaseContext.UserActivities.Update(theUserActivity);
        //    if (IsSingleSaveStyle()) currentUserDatabaseContext.SaveChanges();
        //}

        //public int RemoveAllUserActivities()
        //{
        //    var userActivities = currentUserDatabaseContext.UserActivities.ToList();
        //    int cnt = 0;
        //    foreach (UserActivityModel userActivity in userActivities)
        //    {
        //        currentUserDatabaseContext.UserActivities.Remove(userActivity);
        //        cnt++;
        //    }
        //    currentUserDatabaseContext.SaveChanges();
        //    return cnt;
        //}

        //public IEnumerable<UserActivityModel> GetListOfUserActivities()
        //{
        //    return currentUserDatabaseContext.UserActivities;
        //}

        //private bool IsSingleSaveStyle()
        //{
        //    return databaseSaverStyle == SaverStyle.SINGLE_SAVE;
        //}
        //private void StartDtabeseMultiChanges()
        //{
        //    databaseSaverStyle = SaverStyle.MULTI_SAVE;
        //}
        //private void SaveDatabaseContextChanges()
        //{
        //    databaseSaverStyle = SaverStyle.SINGLE_SAVE;
        //    currentUserDatabaseContext.SaveChanges();
        //}
        //private IEnumerable<UserActivityModel> GetStartedActivitiesWhoOlderWhen(int maxDurationMinutes = 240)
        //{
        //    DateTime droptime = DateTime.Now.AddMinutes(-1 * maxDurationMinutes);
        //    return currentUserDatabaseContext.UserActivities
        //        .Where(a => a.Status == UserActivityModel.ActivityStatus.Started && a.StartTime < droptime)
        //        .OrderByDescending(a => a.StartTime);
        //}
    }

}

