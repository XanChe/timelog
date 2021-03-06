using System.ComponentModel.DataAnnotations;

namespace Timelog.AspNetCore.CommandRequests
{    
    public class SaveProjectRequest
    {
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
