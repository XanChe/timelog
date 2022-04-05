using System;
using System.ComponentModel.DataAnnotations;

namespace Timelog.Entities
{
    public class UserActivityModel : Entity
    {
        public UserActivityModel() { }
        public UserActivityModel(UserActivityModel other, Project project, ActivityType activityType)
        {
            this.Title = other.Title;
            this.StartTime = other.StartTime;
            this.EndTime = other.EndTime;
            this.Comment = other.Comment;
            this.Id = other.Id;
            this.UniqId = other.UniqId;
            this.UserUniqId = other.UserUniqId;
            this.Status = other.Status;
            this.Project = project;
            this.ActivityType = activityType;
        }

        public enum ActivityStatus
        {
            Draft,
            Started,
            Complite
        }

        [Display(Name = "Начало")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Окончание")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Статус")]
        public ActivityStatus Status { get; set; } = ActivityStatus.Draft;
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
        public long ProjectId { get; set; }
#nullable enable
        [Display(Name = "Проект")] 
        public Project? Project { get; set; }
        [Display(Name = "Тип деятельности")]
        public ActivityType? ActivityType { get; set; }
#nullable restore
        public long ActivityTypeId { get; set; }

    }
}

