using System.ComponentModel.DataAnnotations;
using Timelog.Core.Entities;

namespace Timelog.AspNetCore.ViewModels
{
    public class UserActivityViewModel
    {
        public static implicit operator UserActivityViewModel?(UserActivity? activity)
        {
            if (activity != null)
            {
                return new UserActivityViewModel()
                {
                    Title = activity.Title,
                    StartTime = activity.StartTime,
                    EndTime = activity.EndTime,
                    Comment = activity.Comment,
                    Id = activity.Id,
                    Status = activity.Status.ToString()                    
                };
            }
            return null;                     
            
        }
        public Guid Id { get; set; }
        [Display(Name = "Начало")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Окончание")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Статус")]
        public string Status { get; set; } = String.Empty;
        [Display(Name = "Заголовок")]
        public string Title { get; set; } = String.Empty;
        [Display(Name = "Комментарий")]
        public string Comment { get; set; } = String.Empty;
        public Guid ProjectId { get; set; }
        [Display(Name = "Проект")]
        public string Project { get; set; } = String.Empty;
        [Display(Name = "Тип деятельности")] 
        public string ActivityType { get; set; } = String.Empty;

        public Guid ActivityTypeId { get; set; }
    }
}
