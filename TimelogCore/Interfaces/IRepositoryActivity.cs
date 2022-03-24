using Timelog.Entities;

namespace Timelog.Interfaces
{
    public interface IRepositoryActivity : IRepositoryGeneric<UserActivityModel>  
    {
        //public void SetFilterByUser(string userUniqId);
        public UserActivityModel getCurrentActivity();
    }
}
