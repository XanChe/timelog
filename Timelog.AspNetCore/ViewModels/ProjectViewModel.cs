using System.ComponentModel.DataAnnotations;
using Timelog.Core.Entities;

namespace Timelog.AspNetCore.ViewModels
{
   
    public class ProjectViewModel
    {
        [Display(Name = "Название")]
        public string Name { get; set; } = String.Empty;
        [Display(Name = "Описание")]
        public string Description { get; set; } = String.Empty;
    }
}
