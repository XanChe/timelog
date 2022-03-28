using System.ComponentModel.DataAnnotations;

namespace Timelog.WebApp.ViewModels
{
    public class ActivityViewModel
    {
        
        
        public enum ActivityStatus
        {
            Draft,
            Started,
            Complite
        }

        public long Id { get; set; }

        [Display(Name = "Старт")]
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public ActivityStatus Status { get; set; } = ActivityStatus.Draft;

        public string Title { get; set; }

        public string Comment { get; set; }

        public string ProjectName { get; set; }
        public long ProjectId { get; set; }
        public string ActivityTypeName { get; set; }
        public long ActivityTypeId { get; set; }
    }
}
