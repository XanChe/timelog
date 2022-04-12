using System.ComponentModel.DataAnnotations;

namespace Timelog.AspNetCore.ViewModels
{
    public class ActivityTypeViewModel
    {
        [Display(Name = "Название")]
        public string Name { get; set; } = String.Empty;
        [Display(Name = "Описание")]
        public string Description { get; set; } = String.Empty;
    }
}
