using System;


namespace Timelog.Entities
{
    public class UserActivityModel : Entity
    {
        public enum ActivityStatus
        {
            Draft,
            Started,
            Complite
        }
       
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public ActivityStatus Status { get; set; } = ActivityStatus.Draft;

        public string Title { get; set; }

        public string Comment { get; set; }
        public long ProjectId { get; set; }
        public long ActivityTypeId { get; set; }

    }
}  

