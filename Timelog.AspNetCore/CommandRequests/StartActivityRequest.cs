using System.ComponentModel.DataAnnotations;


namespace Timelog.AspNetCore.CommandRequests
{
    public class StartActivityRequest
    {
        [Required(ErrorMessage = "Не указан проекст")]
        public Guid ProjectId { get; set; }
        [Required(ErrorMessage = "Не указан тип активности")]
        public Guid ActivityTypeId { get; set; }       
    }
}
