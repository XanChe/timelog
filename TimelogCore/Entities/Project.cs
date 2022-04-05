using System;
using System.ComponentModel.DataAnnotations;

namespace Timelog.Entities
{
  
    public class Project : Entity
    {
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description {get; set; }  

    }

}  

