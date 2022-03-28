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
       
        [Display(Name="������")]
        public DateTime StartTime { get; set; }
        [Display(Name = "���������")]
        public DateTime EndTime { get; set; }
        [Display(Name = "������")]
        public ActivityStatus Status { get; set; } = ActivityStatus.Draft;
        [Display(Name = "���������")]
        public string Title { get; set; }
        [Display(Name = "�����������")]
        public string Comment { get; set; }
        public long ProjectId { get; set; }
        [Display(Name = "������")]
        public Project? Project { get; set; }
        [Display(Name = "��� ������������")]
        public ActivityType? ActivityType { get; set; } 
        public long ActivityTypeId { get; set; }

    }
}  

